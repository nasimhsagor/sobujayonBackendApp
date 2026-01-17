using Microsoft.AspNetCore.Mvc;
using sobujayonApp.Core.ServiceContracts;
using System.Threading.Tasks;

namespace sobujayonApp.API.Controllers
{
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("nav-items")]
        public async Task<IActionResult> GetNavItems()
        {
            return Ok(await _commonService.GetNavItems());
        }

        [HttpGet("delivery-areas")]
        public async Task<IActionResult> GetDeliveryAreas()
        {
            return Ok(await _commonService.GetDeliveryAreas());
        }
    }
}
