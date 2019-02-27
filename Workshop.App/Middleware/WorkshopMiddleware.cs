using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Workshop.App.Middleware
{
    public class WorkshopMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<BlogSettings> _settings;
        private readonly IFileProvider _fileProvider;

        public WorkshopMiddleware(RequestDelegate next, IOptions<BlogSettings> settings, IFileProvider fileProvider)
        {
            _next = next;
            _settings = settings;
            _fileProvider = fileProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/options"))
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(_settings.Value));
            }
            else if (context.Request.Path.StartsWithSegments("/files"))
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(_fileProvider.GetDirectoryContents("").Select(f => f.Name)));
            }
            else
            {
                await _next(context);
            }
        }
    }
}
