using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;
using ManageRevenue.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRevenue.BLL.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserViewModel>> GetAllUsers();
        Task<AuthResponse> RegisterUserAsync(RegisterModel model);
        Task<AuthResponse> LoginUserAsync(LoginModel model);
        bool ValidateJwtToken(string token);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
