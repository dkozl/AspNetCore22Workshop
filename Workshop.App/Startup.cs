﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IBlogContext, BlogContext>(builder => builder.UseSqlServer(_configuration.GetConnectionString("BlogDatabase")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(ctx => ctx.Response.WriteAsync("Hello World!"));
        }
    }
}
