using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.App.Entities;

namespace Workshop.App.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IBlogContext _context;

        public BlogRepository(IBlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Blog>> Read()
        {
            return (await _context.Blogs.Include(blog => blog.Posts).ToArrayAsync())
                .Select(
                    blog => new Models.Blog
                    {
                        Name = blog.Name,
                        Description = blog.Description,
                        Posts = blog.Posts.Select(
                            post => new Models.Post
                            {
                                Title = post.Title,
                                Content = post.Content
                            }).ToArray()
                    })
                .ToArray();
        }
    }
}
