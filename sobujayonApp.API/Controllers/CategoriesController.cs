using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;

namespace sobujayonApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _repo;
        public CategoriesController(ICategoriesRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllCategories());

        [HttpPost]
        [Authorize] // Only admins should create categories
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.Slug))
                category.Slug = category.Name.ToLower().Replace(" ", "-");

            var result = await _repo.AddCategory(category);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
    }
}
