using ManageRevenue.BLL.Interfaces;
using ManageRevenue.BLL.Services;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;
using ManageRevenue.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageRevenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize]
        [HttpPost("add_category")]
        public async Task<IActionResult> AddCategory(CategoryRequestModel categoryRequestModel)
        {
            var request = new CategoryViewModel
            {
                Id = categoryRequestModel.Id,
                UserId = categoryRequestModel.UserId,
                Name = categoryRequestModel.Name,
                Type = categoryRequestModel.Type,
                IsPersonal = categoryRequestModel.IsPersonal,
            };
            var result = await _categoryService.AddCategoryRevenu(request);
            return  Ok(result); ;
        }
        [Authorize]
        [HttpGet("get_category")]
        public async Task<IActionResult> GetCategoryByUser(int id, bool typeCollect, bool typeSpend)
        {
            var categoryResponse = await _categoryService.GetCategoryRevenuByUserId(id, typeCollect,typeSpend);

            var response = new Response<CategoryResponseModel>
            {
                Code = categoryResponse.Code,
                Message = categoryResponse.Message,
                DataList = categoryResponse.DataList.Select(category => new CategoryResponseModel
                {
                    Id = category.Id,
                    UserId = category.UserId,
                    Name = category.Name,
                    IsPersonal = category.IsPersonal,
                    Type = category.Type

                }).ToList()
            };
            return Ok(response);
        }
    }
}
