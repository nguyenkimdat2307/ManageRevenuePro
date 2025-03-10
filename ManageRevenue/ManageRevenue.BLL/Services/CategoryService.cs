﻿using ManageRevenue.BLL.Common;
using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepositoy _categoryRepository;
        private readonly ISessionInfo _sessionInfo;
        public CategoryService(ICategoryRepositoy categoryRepository, ISessionInfo sessionInfo)
        {
            _categoryRepository = categoryRepository;
            _sessionInfo = sessionInfo;
        }
        public async Task<Response<string>> AddCategoryRevenu(CategoryViewModel categoryViewModel)
        {
            var response = new Response<string>();
            int userId = _sessionInfo.GetUserId();

            if (userId == 0)
            {
                response.Code = 401;
                response.Message = "Unauthorized";
                return response;
            }
            var category = new CategoryViewModel
            {
                Name = categoryViewModel.Name,
                Type = categoryViewModel.Type,
                Color = categoryViewModel.Color,
                Icon = categoryViewModel.Icon,
                UserId = userId
            };
            var result = await _categoryRepository.AddCategoryManageRevenue(category);
            response.Message = result.Message;
            response.Code = result.Code;
            return response;
        }

        public async Task<Response<string>> DeleteCategorySummaryId(int categoryId)
        {
            var result = await _categoryRepository.DeleteCategoryManageRevenue(categoryId);
            return result;
        }

        public async Task<Response<CategoryViewModel>> GetCategoryByIdSummary(int categoryId)
        {
            var result = await _categoryRepository.GetCategoryById(categoryId);
            return result;
        }

        public async Task<Response<CategoryViewModel>> GetCategoryRevenuByUserId()
        {
            var response = new Response<CategoryViewModel>();
            int userId = _sessionInfo.GetUserId();

            if (userId == 0)
            {
                response.Code = 401;
                response.Message = "Unauthorized";
                return response;
            }
            var result = await _categoryRepository.GetCategoryManageRevenue(userId);
            response.DataList = result.DataList;
            return response;
        }

        public async Task<Response<string>> UpdateCategoryManageRevenue(CategoryViewModel categoryViewModel)
        {
            var response = new Response<string>();
            int userId = _sessionInfo.GetUserId();

            if (userId == 0)
            {
                response.Code = 401;
                response.Message = "Unauthorized";
                return response;
            }
            var param = new CategoryViewModel
            {
                Id = categoryViewModel.Id,
                UserId = userId,
                Color = categoryViewModel.Color,
                Icon = categoryViewModel.Icon,
                Type = categoryViewModel.Type,
                Name = categoryViewModel.Name
            };
            var result = await _categoryRepository.UpdateCategoryManageRevenue(param);
            response.Message = result.Message;
            return response;
        }
    }
}
