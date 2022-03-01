using Microsoft.AspNetCore.Mvc;

namespace HotelManager.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("healthcheck")]
        public IActionResult Health()
        {
            return Ok("OK");
        }
    }
}
