using Dapper;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ManageRevenue.DAL.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepositoy
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<Response<string>> AddCategoryManageRevenue(CategoryViewModel categoryViewModel)
        {
            var response = new Response<string>();
            using var connection = CreateConnection();

            string sql = @"INSERT INTO Categories (UserId, Name, Type, Color, Icon, CreatedAt, UpdatedAt) 
                   VALUES (@UserId, @Name, @Type, @Color, @Icon, GETDATE(), GETDATE());";

            await connection.ExecuteAsync(sql, categoryViewModel);

            response.Message = "Category added successfully.";
            return response;
        }

        public async Task<Response<string>> DeleteCategoryManageRevenue(int categoryId)
        {
            var response = new Response<string>();
            using var connection = CreateConnection();

            string sql = "DELETE FROM Categories WHERE Id = @CategoryId";

            int rowsAffected = await connection.ExecuteAsync(sql, new { CategoryId = categoryId });

            response.Message = rowsAffected > 0 ? "Category deleted successfully." : "Category not found.";
            return response;
        }

        public async Task<Response<CategoryViewModel>> GetCategoryById(int categoryId)
        {
            var response = new Response<CategoryViewModel>();
            using var connection = CreateConnection();

            string sql = @"SELECT Id, UserId, Name, Type, CreatedAt, UpdatedAt, Color, Icon 
                   FROM Categories 
                   WHERE Id = @CategoryId";

            var categoryById = await connection.QueryFirstOrDefaultAsync<CategoryViewModel>(sql, new { CategoryId = categoryId });
            response.Data = categoryById;
            response.Message = "Successfully retrieved categories.";
            response.Code = StatusCodes.Status200OK;
            return response;
        }

        public async Task<Response<CategoryViewModel>> GetCategoryManageRevenue(int userId)
        {
            var response = new Response<CategoryViewModel>();
            using var connection = CreateConnection();

            string sql = @"
                SELECT Id, UserId, Name, Type, Color, Icon, CreatedAt, UpdatedAt
                FROM Categories
                WHERE UserId = @UserId;
            ";

            var categories = (await connection.QueryAsync<CategoryViewModel>(sql, new
            {
                UserId = userId,
            })).ToList();

            response.DataList = categories.ToList();
            response.Message = "Successfully retrieved categories.";
            response.Code = StatusCodes.Status200OK;
            return response;
        }


        public async Task<Response<string>> UpdateCategoryManageRevenue(CategoryViewModel categoryViewModel)
        {
            var response = new Response<string>();
            using var connection = CreateConnection();

            string sql = @"
                  UPDATE Categories 
                    SET Name = @Name, 
                    Type = @Type, 
                    Color = @Color,
                    Icon = @Icon,
                    UpdatedAt = GETDATE()
                    WHERE Id = @Id AND UserId = @UserId";

            int rowsAffected = await connection.ExecuteAsync(sql, categoryViewModel);

            response.Message = rowsAffected > 0 ? "Category updated successfully." : "Category not found or you don't have permission.";
            return response;
        }
    }
}
