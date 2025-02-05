using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManageRevenue.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _userService.RegisterUserAsync(model);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.LoginUserAsync(model);
            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result);
        }
    }
}
