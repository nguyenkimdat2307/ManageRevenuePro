using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<string>> AddCategoryRevenu(CategoryViewModel categoryViewModel);
        Task<Response<CategoryViewModel>> GetCategoryRevenuByUserId();
    }
}
