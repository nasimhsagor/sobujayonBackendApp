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
    [Route("orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private Guid GetUserId()
        {
            var claim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) throw new UnauthorizedAccessException("User ID not found in token");
            return Guid.Parse(claim.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var result = await _orderService.CreateOrder(GetUserId(), request);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderService.GetOrders(GetUserId()));
        }
    }
}
