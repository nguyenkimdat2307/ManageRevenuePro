using ManageRevenue.BLL.Services;
using ManageRevenue.Domain.Common;
using ManageRevenue.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManageRevenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersResponse = await _userService.GetAllUsers();  

            var response = new Response<UserResponseModel>
            {
                Code = usersResponse.Code,  
                Message = usersResponse.Message,  
                DataList = usersResponse.DataList.Select(user => new UserResponseModel
                {
                    Id = user.Id,
                    Username = user.Username
                }).ToList()
            };

            return Ok(response);
        }


    }
}
