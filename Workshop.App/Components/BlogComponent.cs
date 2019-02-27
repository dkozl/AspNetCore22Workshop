using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.App.Entities;

namespace Workshop.App.Components
{
    [ViewComponent(Name = "Blog")]
    public class BlogComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(Blog blog)
        {
            return Task.FromResult((IViewComponentResult) View(blog));
        }
    }
}
