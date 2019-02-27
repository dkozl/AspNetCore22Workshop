using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<WorkshopMiddleware>();
            app.Map("/workshop", builder => builder.Run(ctx => ctx.Response.WriteAsync("middleware 2")));

            app.Run(ctx => ctx.Response.WriteAsync("Hello World!"));
        }
    }
}
