using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Workshop.App.Entities
{
    public interface IBlogContext
    {
        DbSet<Blog> Blogs { get; set; }

        DbSet<Post> Posts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
