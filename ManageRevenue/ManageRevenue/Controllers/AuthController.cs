using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Models;
using ManageRevenue.Domain.Models.Auth;
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

        [HttpGet("validate-token")]
        public  IActionResult ValidateToken()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return Unauthorized(new { message = "Token is missing" });

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var isValid =  _userService.ValidateJwtToken(token);
            if (!isValid)
                return Unauthorized(new { message = "Invalid token" });

            return Ok(new { message = "Token is valid" });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await _userService.RefreshTokenAsync(request);
            if (!response.Success)
                return Unauthorized(new { message = response.Message });

            return Ok(response);
        }


    }
}
