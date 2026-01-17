using Microsoft.AspNetCore.Mvc;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.ServiceContracts;
using System.Threading.Tasks;

namespace sobujayonApp.API.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoriesService.GetAllCategories());
        }

        [HttpGet("{slug}/products")]
        public async Task<IActionResult> GetProducts(string slug)
        {
            return Ok(await _categoriesService.GetCategoryProducts(slug));
        }
    }
}
