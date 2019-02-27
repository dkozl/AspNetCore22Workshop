using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Workshop.App.Middleware
{
    public class WorkshopMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<BlogSettings> _settings;

        public WorkshopMiddleware(RequestDelegate next, IOptions<BlogSettings> settings)
        {
            _next = next;
            _settings = settings;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/options"))
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(_settings.Value));
            }
            else
            {
                await _next(context);
            }
        }
    }
}
