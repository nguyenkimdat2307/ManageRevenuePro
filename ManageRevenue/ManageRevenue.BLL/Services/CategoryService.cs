using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepositoy _categoryRepository;
        public CategoryService(ICategoryRepositoy categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<string>> AddCategoryRevenu(CategoryViewModel categoryViewModel)
        {
            var response = new Response<string>();
            var category = new CategoryViewModel
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
                Type = categoryViewModel.Type,
                UserId = categoryViewModel.UserId
            };
            var result = await _categoryRepository.AddCategoryManageRevenue(category);
            response.Message = "Category added successfully.";
            return response;
        }

        public async Task<Response<CategoryViewModel>> GetCategoryRevenuByUserId(int userId)
        {
            var respone = new Response<CategoryViewModel>();
            var result = await _categoryRepository.GetCategoryManageRevenue(userId);
            respone.DataList = result.DataList;
            return respone;
        }
    }
}
