using Microsoft.EntityFrameworkCore;

namespace Workshop.App.Entities
{
    public class BlogContext : DbContext, IBlogContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Blog>().HasData(
                new Blog {BlogId = 1, Name = "Workshop blog", Description = "Test blog"}
            );

            modelBuilder.Entity<Post>().HasData(
                new Post {PostId = 1, BlogId = 1, Content = "ASP.NET Core workshop with EF", Title = "Initial blog entry"}
            );
        }
    }
}