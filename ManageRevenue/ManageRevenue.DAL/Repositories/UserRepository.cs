using Dapper;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ManageRevenue.DAL.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<Response<string>> CreateUserAsync(UserViewModel user)
        {
            var response = new Response<string>();
            using var connection = CreateConnection();
            string sql = "INSERT INTO Users (Username, Email, PasswordHash, CreatedAt) VALUES (@Username, @Email, @PasswordHash, @CreatedAt)";
            await connection.ExecuteAsync(sql, user);
            response.Message = "Successfully retrieved users.";
            return response;
        }

        public async Task<Response<UserViewModel>> GetAllUsers()
        {
            var response = new Response<UserViewModel>();  

            using var connection = CreateConnection();
            var users = await connection.QueryAsync<UserViewModel>("SELECT * FROM Users");  

            response.DataList = users.ToList();  
            response.Message = "Successfully retrieved users."; 
            response.Code = StatusCodes.Status200OK;  

            return response;
        }

        public async Task<Response<UserViewModel>> GetUserByUsernameAsync(string username)
        {
            var response = new Response<UserViewModel>();
            using var connection = CreateConnection();
            string sql = "SELECT * FROM Users WHERE Username = @Username";
            var user = await connection.QueryFirstOrDefaultAsync<UserViewModel>(sql, new { Username = username });
            response.Data = user;
            response.Message = "Successfully retrieved users.";
            response.Code = StatusCodes.Status200OK;
            return response;
        }
    }
}
