using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.Domain.Interfaces
{
    public interface ICategoryRepositoy
    {
        Task<Response<string>> AddCategoryManageRevenue(CategoryViewModel categoryViewModel);
        Task<Response<string>> DeleteCategoryManageRevenue(int categoryId);
        Task<Response<string>> UpdateCategoryManageRevenue(CategoryViewModel categoryViewModel);
        Task<Response<CategoryViewModel>> GetCategoryManageRevenue(int userId);
        Task<Response<CategoryViewModel>> GetCategoryById(int categoryId);
    }
}
