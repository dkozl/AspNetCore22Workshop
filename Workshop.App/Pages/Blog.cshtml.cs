using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Workshop.App.Entities;

namespace Workshop.App.Pages
{
    public class BlogModel : PageModel
    {
        private readonly IBlogContext _context;

        public IEnumerable<Blog> BlogEntries { get; set; }

        public BlogModel(IBlogContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            BlogEntries = await _context.Blogs.Include(blog => blog.Posts).ToArrayAsync();
        }

    }
}