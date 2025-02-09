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
        [HttpPost("new-category")]
        public async Task<IActionResult> AddCategory(CategoryRequestModel categoryRequestModel)
        {
            var request = new CategoryViewModel
            {
                Name = categoryRequestModel.Name,
                Type = categoryRequestModel.Type,
                Color = categoryRequestModel.Color,
                Icon = categoryRequestModel.Icon
            };
            var result = await _categoryService.AddCategoryRevenu(request);
            return Ok(result); ;
        }
        [Authorize]
        [HttpGet("get-category")]
        public async Task<IActionResult> GetCategoryByUser()
        {
            var categoryResponse = await _categoryService.GetCategoryRevenuByUserId();

            var response = new Response<CategoryResponseModel>
            {
                Code = categoryResponse.Code,
                Message = categoryResponse.Message,
                DataList = new List<CategoryResponseModel>
        {
            new CategoryResponseModel
            {
                ListCategoryCollect = categoryResponse.DataList
                    .Where(category => category.Type == true)
                    .Select(category => new CategoryCollect
                    {
                        Id = category.Id,
                        UserId = category.UserId,
                        Name = category.Name,
                        Type = category.Type,
                        Color = category.Color,
                        Icon = category.Icon
                    }).ToList(),

                ListCategorySpend = categoryResponse.DataList
                    .Where(category => category.Type == false)
                    .Select(category => new CategorySpend
                    {
                        Id = category.Id,
                        UserId = category.UserId,
                        Name = category.Name,
                        Type = category.Type,
                        Color = category.Color,
                        Icon = category.Icon
                    }).ToList()
            }
        }
            };

            return Ok(response);
        }

        [Authorize]
        [HttpGet("get-by-id-category")]
        public async Task<IActionResult> GetCategoryByID(int categoryId)
        {
            var result = await _categoryService.GetCategoryByIdSummary(categoryId);
            return Ok(result); ;
        }
        [Authorize]
        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategoryByID(int categoryId)
        {
            var result = await _categoryService.DeleteCategorySummaryId(categoryId);
            return Ok(result); ;
        }
        [Authorize]
        [HttpPost("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequestModel categoryRequestModel)
        {
            var request = new CategoryViewModel
            {
                Id = categoryRequestModel.Id,
                Name = categoryRequestModel.Name,
                Type = categoryRequestModel.Type,
                Color = categoryRequestModel.Color,
                Icon = categoryRequestModel.Icon
            };
            var result = await _categoryService.UpdateCategoryManageRevenue(request);
            return Ok(result); ;
        }
    }
}
