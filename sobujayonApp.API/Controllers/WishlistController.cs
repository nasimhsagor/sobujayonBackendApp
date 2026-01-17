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
    [Route("wishlist")]
    [ApiController]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        private Guid GetUserId()
        {
            var claim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) throw new UnauthorizedAccessException("User ID not found in token");
            return Guid.Parse(claim.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddWishlistItemRequest request)
        {
            // request is json object { productId: "plant-1" }. 
            // I should create AddWishlistItemRequest DTO or just use dynamic or class.
            // Inline class
            if (string.IsNullOrEmpty(request.ProductId)) return BadRequest();
            await _wishlistService.AddToWishlist(GetUserId(), request.ProductId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _wishlistService.GetWishlist(GetUserId()));
        }
    }
    
    public class AddWishlistItemRequest {
        public string ProductId { get; set; }
    }
}
