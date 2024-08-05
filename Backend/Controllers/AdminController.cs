using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieSugges.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("manage")]
        [Authorize(Roles = "Admin")]
        public IActionResult Manage()
        {
            return Ok("This is an admin area.");
        }
    }
}
