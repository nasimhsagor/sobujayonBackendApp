using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.ServiceContracts;

namespace sobujayonApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _productsService.GetAllProducts());

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var result = await _productsService.CreateProduct(product);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}
