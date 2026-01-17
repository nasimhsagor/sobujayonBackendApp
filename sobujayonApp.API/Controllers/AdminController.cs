using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.ServiceContracts;
using System.Threading.Tasks;

namespace sobujayonApp.API.Controllers
{
    [Route("admin")]
    [ApiController]
    [Authorize] // Require Auth
    public class AdminController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IBlogService _blogService;

        public AdminController(IProductsService productsService, ICategoriesService categoriesService, IBlogService blogService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _blogService = blogService;
        }

        [HttpPost("Products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var result = await _productsService.CreateProduct(request);
            return CreatedAtAction("GetBySlug", "Products", new { slug = result.Slug }, result);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var result = await _categoriesService.CreateCategory(request);
            return Created("", result);
        }

        [HttpPost("blogs")]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogRequest request)
        {
            var result = await _blogService.CreateBlog(request);
            return Created("", result);
        }

        [HttpPut("blogs/{id}")]
        public async Task<IActionResult> UpdateBlog(string id, [FromBody] CreateBlogRequest request)
        {
            var result = await _blogService.UpdateBlog(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("blogs/{id}")]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            var success = await _blogService.DeleteBlog(id);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
