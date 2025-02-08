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

            string sql = @"INSERT INTO Categories (UserId, Name, Type, IsPersonal, CreatedAt, UpdatedAt) 
                   VALUES (@UserId, @Name, @Type, @IsPersonal, GETDATE(), GETDATE());";

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


        public async Task<Response<CategoryViewModel>> GetCategoryManageRevenue(int userId, bool typeCollect, bool typeSpend)
        {
            var response = new Response<CategoryViewModel>();
            using var connection = CreateConnection();

            string sql = @"
                SELECT Id, UserId, Name, Type, IsPersonal, CreatedAt, UpdatedAt
                FROM Categories
                WHERE IsPersonal = 0 AND (Type = @TypeCollect OR Type = @TypeSpend)
                UNION
                SELECT Id, UserId, Name, Type, IsPersonal, CreatedAt, UpdatedAt
                FROM Categories
                WHERE IsPersonal = 1 AND UserId = @UserId AND (Type = @TypeCollect OR Type = @TypeSpend);
            ";

            var categories = (await connection.QueryAsync<CategoryViewModel>(sql, new
            {
                UserId = userId,
                TypeCollect = typeCollect ? 1 : -1, 
                TypeSpend = typeSpend ? 0 : -1 
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
                    IsPersonal = @IsPersonal, 
                    UpdatedAt = GETDATE()
                    WHERE Id = @Id AND UserId = @UserId";

            int rowsAffected = await connection.ExecuteAsync(sql, categoryViewModel);

            response.Message = rowsAffected > 0 ? "Category updated successfully." : "Category not found or you don't have permission.";
            return response;
        }

    }
}
