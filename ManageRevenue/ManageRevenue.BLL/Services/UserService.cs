using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using ManageRevenue.Domain.Models.Auth;
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

            var accessToken = GenerateJwtToken(user, TimeSpan.FromMinutes(15)); 
            var refreshToken = GenerateJwtToken(user, TimeSpan.FromDays(7));

            return new AuthResponse
            {
                Success = true,
                Token = accessToken,
                RefreshToken = refreshToken,
                UserName = model.Username
            };
        }
        
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = false
                }, out SecurityToken securityToken);

                if (securityToken is JwtSecurityToken jwtSecurityToken)
                {
                    var alg = jwtSecurityToken.Header.Alg;
                    if (alg != SecurityAlgorithms.HmacSha256)
                        return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private string GenerateJwtToken(UserViewModel user, TimeSpan expiration)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.Add(expiration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool ValidateJwtToken(string token)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.RefreshToken);
            if (principal == null)
                return new AuthResponse { Success = false, Message = "Invalid refresh token" };

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return new AuthResponse { Success = false, Message = "Invalid user" };

            var newAccessToken = GenerateJwtToken(new UserViewModel { Id = int.Parse(userId) }, TimeSpan.FromMinutes(15));
            var newRefreshToken = GenerateJwtToken(new UserViewModel { Id = int.Parse(userId) }, TimeSpan.FromDays(7));

            return new AuthResponse
            {
                Success = true,
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

    }
}
