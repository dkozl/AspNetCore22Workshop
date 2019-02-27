using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Workshop.App.Controllers;
using Xunit;

namespace Workshop.App.Tests.Controllers
{
    public class BlogControllerTests
    {
        [Fact]
        public void ShouldBeAssignableToControllerBase()
        {
            typeof(BlogController).Should().BeAssignableTo<ControllerBase>();
        }

        [Fact]
        public void ShouldBeDecoratedWithApiControllerAttribute()
        {
            typeof(BlogController).Should().BeDecoratedWith<ApiControllerAttribute>();
        }

        [Fact]
        public void ShouldBeDecoratedWithRouteAttribute()
        {
            typeof(BlogController).Should().BeDecoratedWith<RouteAttribute>(attr => attr.Template == "api/blog");
        }
    }
}
