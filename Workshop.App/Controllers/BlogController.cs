using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Workshop.App.Repositories;

namespace Workshop.App.Controllers
{
    [ApiController]
    [Route("api/blog")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogRepository blogRepository, ILogger<BlogController> logger)
        {
            _blogRepository = blogRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get all blog entries");
            return Ok(await _blogRepository.Read());
        }
    }
}