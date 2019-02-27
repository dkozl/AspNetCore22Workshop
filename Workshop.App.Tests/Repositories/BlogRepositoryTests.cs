using FluentAssertions;
using Workshop.App.Repositories;
using Xunit;

namespace Workshop.App.Tests.Repositories
{
    public class BlogRepositoryTests
    {
        [Fact]
        public void ShouldBeAssignableToIBlogRepository()
        {
            typeof(BlogRepository).Should().BeAssignableTo<IBlogRepository>();
        }
    }
}
