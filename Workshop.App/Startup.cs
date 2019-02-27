using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Workshop.App.Entities;
using Workshop.App.Middleware;

namespace Workshop.App
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<WorkshopMiddleware>();
            app.Map("/workshop", builder => builder.Run(ctx => ctx.Response.WriteAsync("middleware 2")));

            app.UseHealthChecks("/healthcheck", new HealthCheckOptions {Predicate = hc => hc.Tags.Contains("db")});

            app.Run(ctx => ctx.Response.WriteAsync("Hello World!"));
        }
    }
}
