using Microsoft.AspNetCore.Mvc;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.ServiceContracts;
using System.Threading.Tasks;

namespace sobujayonApp.API.Controllers
{
    [Route("blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            return Ok(await _blogService.GetBlogs(search, page, limit));
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var result = await _blogService.GetBlogBySlug(slug);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
