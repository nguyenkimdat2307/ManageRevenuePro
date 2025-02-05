using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageRevenue.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public Task<Response<UserViewModel>> GetAllUsers() => _userRepository.GetAllUsers();

        public async Task<AuthResponse> RegisterUserAsync(RegisterModel model)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(model.Username);
            if (existingUser.Data != null)
                return new AuthResponse { Success = false, Message = "User already exists" };

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var user = new UserViewModel { Username = model.Username, Email = model.Email, PasswordHash = hashedPassword, CreatedAt = DateTime.UtcNow };

            await _userRepository.CreateUserAsync(user);
            return new AuthResponse { Success = true, Message = "User registered successfully" };
        }

        public async Task<AuthResponse> LoginUserAsync(LoginModel model)
        {
            var response = await _userRepository.GetUserByUsernameAsync(model.Username);
            var user = response.Data;
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return new AuthResponse { Success = false, Message = "Invalid credentials" };

            var token = GenerateJwtToken(user);
            return new AuthResponse { Success = true, Token = token };
        }

        private string GenerateJwtToken(UserViewModel user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
