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
    }
}
