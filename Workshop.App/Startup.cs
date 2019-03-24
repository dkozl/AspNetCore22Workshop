using System;
using System.Data;
using System.Data.SqlClient;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Workshop.App.Entities;
using Workshop.App.Middleware;
using Workshop.App.Repositories;

namespace Workshop.App
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IBlogContext, BlogContext>(builder => builder.UseSqlServer(_configuration.GetConnectionString("BlogDatabase")));
            services.AddHealthChecks().AddAsyncCheck(
                "DB Check",
                async ct =>
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("BlogDatabase")))
                    {
                        await connection.OpenAsync(ct);
                        if (connection.State == ConnectionState.Open)
                        {
                            using (var command = new SqlCommand("select count(*) from Blogs", connection))
                            {
                                if ((int) await command.ExecuteScalarAsync(ct) > 0) return HealthCheckResult.Healthy();
                            }
                        }

                        return HealthCheckResult.Unhealthy();
                    }
                },
                new[] {"db"});

            services.Configure<BlogSettings>(_configuration.GetSection("BlogSettings"));
            services.Configure<BlogSettings>(settings => settings.Properties.Add("NewProperty", "some value"));

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider("/"));

            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new Info()));

            services.AddMvc();

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterType<BlogRepository>().As<IBlogRepository>();

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<WorkshopMiddleware>();
            app.Map("/workshop", builder => builder.Run(ctx => ctx.Response.WriteAsync("middleware 2")));

            app.UseHealthChecks("/healthcheck", new HealthCheckOptions {Predicate = hc => hc.Tags.Contains("db")});

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workshop API"));

            app.UseMvc();
        }
    }
}
