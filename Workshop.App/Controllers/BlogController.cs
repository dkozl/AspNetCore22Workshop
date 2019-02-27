using Microsoft.AspNetCore.Mvc;
using Workshop.App.Repositories;

namespace Workshop.App.Controllers
{
    [ApiController]
    [Route("api/blog")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
    }
}