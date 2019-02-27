using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Workshop.App.Middleware
{
    public class WorkshopMiddleware
    {
        private readonly RequestDelegate _next;

        public WorkshopMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/middleware"))
            {
                await context.Response.WriteAsync("Middleware 1");
            }
            await _next(context);
        }
    }
}
