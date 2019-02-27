using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Workshop.App.Tests
{
    public class StartupTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _applicationFactory;

        public StartupTests(WebApplicationFactory<Startup> applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }

        [Fact]
        public async Task MiddlewareTests()
        {
            var client = _applicationFactory.CreateClient();

            var response = await client.GetAsync("/swagger/v1/swagger.json");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
