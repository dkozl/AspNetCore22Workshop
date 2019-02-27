using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    }
}