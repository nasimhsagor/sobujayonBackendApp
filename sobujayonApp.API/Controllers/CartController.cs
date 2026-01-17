using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.ServiceContracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sobujayonApp.API.Controllers
{
    [Route("cart")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private Guid GetUserId()
        {
            var claim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) throw new UnauthorizedAccessException("User ID not found in token");
            return Guid.Parse(claim.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cartService.GetCart(GetUserId()));
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddCartItemRequest request)
        {
            await _cartService.AddToCart(GetUserId(), request);
            return Ok();
        }
    }
}
