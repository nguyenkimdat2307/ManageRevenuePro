using Dapper;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using ManageRevenue.Domain.Models.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Transactions;

namespace ManageRevenue.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Response<string>> AddTransactionRevenue(TransactionViewModel transactionViewModel)
        {
            var response = new Response<string>();
            using var connection = CreateConnection();

            // Gọi stored procedure AddTransaction
            var parameters = new
            {
                UserId = transactionViewModel.UserId,
                CategoryId = transactionViewModel.CategoryId,
                Amount = transactionViewModel.Amount,
                Description = transactionViewModel.Description,
                TransactionType = transactionViewModel.TransactionType,
                Date = transactionViewModel.Date
            };

            // Thực thi stored procedure
            await connection.ExecuteAsync(
                "AddTransaction",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            response.Message = "Transaction added successfully.";
            return response;
        }

        public async Task<Response<string>> DeleteTransactionRevenue(int transactionId, int userId)
        {
            var response = new Response<string>();
            using var connection = CreateConnection();

            var parameters = new { TransactionId = transactionId, UserId = userId };

            await connection.ExecuteAsync("DeleteTransaction", parameters, commandType: CommandType.StoredProcedure);

            response.Message = "Transaction deleted successfully.";
            return response;
        }

        public async Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int userId, int month, int year)
        {
            var response = new TransactionSummaryViewModel();
            using var connection = CreateConnection();

            var multi = await connection.QueryMultipleAsync(
                "GetMonthlyTransactionSummary",
                new { UserId = userId, Month = month, Year = year },
                commandType: CommandType.StoredProcedure
            );

           
            var transactionDetails = (await multi.ReadAsync<TransactionDetail>()).ToList();
            if (!transactionDetails.Any())
            {
                Console.WriteLine("No transaction details returned.");
            }

            var summaryData = await multi.ReadFirstOrDefaultAsync<TransactionSummaryData>();
            if (summaryData == null)
            {
                Console.WriteLine("No summary data returned.");
            }
            else
            {
                Console.WriteLine($"TotalIncome: {summaryData.TotalIncome}, TotalExpense: {summaryData.TotalExpense}, RemainingBalance: {summaryData.RemainingBalance}");
            }


            response.Data = summaryData;  
            response.DataList = transactionDetails;
            response.Message = "Successfully retrieved monthly transaction summary.";
            response.Code = 200;

            return response;
        }

        public async Task<Response<TransactionDetailViewModel>> GetTransactionById(int transactionId)
        {
            var response = new Response<TransactionDetailViewModel>();
            using var connection = CreateConnection();

            string sql = @"SELECT t.Id, t.UserId, t.CategoryId, c.Name AS CategoryName, 
                          t.Amount, t.Description, t.Date, t.CreatedAt, t.UpdatedAt
                   FROM Transactions t
                   LEFT JOIN Categories c ON t.CategoryId = c.Id
                   WHERE t.Id = @TransactionId";

            var result =  await connection.QueryFirstOrDefaultAsync<TransactionDetailViewModel>(sql, new { TransactionId = transactionId });
            response.Data = result;
            response.Message = "Successfully retrieved transation.";
            response.Code = StatusCodes.Status200OK;
            return response;
        }

        public async Task<Response<string>> UpdateTransactionRevenue(TransactionViewModel transactionViewModel)
        {
            var response = new Response<string>();
            using var connection = CreateConnection();

            // Gọi stored procedure UpdateTransaction
            var parameters = new
            {
                TransactionId = transactionViewModel.Id,
                UserId = transactionViewModel.UserId,
                CategoryId = transactionViewModel.CategoryId,
                Description = transactionViewModel.Description,
                TransactionType = transactionViewModel.TransactionType,
                NewAmount = transactionViewModel.NewAmount,
                NewDate = transactionViewModel.Date
            };

            // Thực thi stored procedure
            await connection.ExecuteAsync(
                "UpdateTransaction",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            response.Message = "Transaction update successfully.";
            return response;
        }
        public async Task<Response<TransactionStatisticsSummaryViewModel>> GetTransactionStatisticsSummary(int userId, int year,int month)
        {
            var response = new Response<TransactionStatisticsSummaryViewModel>();
            using var connection = CreateConnection();

            var multi = await connection.QueryMultipleAsync(
                "GetAnnualTransactionSummary",
                new { UserId = userId, Year = year, Month = month },
                commandType: CommandType.StoredProcedure
            );

            var listSpend = (await multi.ReadAsync<TransactionStatisticsSpendandCollect>()).ToList();

            var listCollect = (await multi.ReadAsync<TransactionStatisticsSpendandCollect>()).ToList();

            var transactionStatistics = await multi.ReadFirstOrDefaultAsync<TransactionStatistics>();

            // Gán dữ liệu vào ViewModel
            response.DataList = new List<TransactionStatisticsSummaryViewModel>
            {
                new TransactionStatisticsSummaryViewModel{TotalStatistics = transactionStatistics,listSpend = listSpend, listCollect = listCollect}
            };
            response.Message = "Successfully retrieved transaction statistics.";
            return response;
        }

        public async Task<Response<TransactionForCategoryResponseModel>> GetTransactionForCategorySummary(TransactionForCategoryRequestModel requestModel)
        {
            var response = new Response<TransactionForCategoryResponseModel>();
            using var connection = CreateConnection();

            var multi = await connection.QueryMultipleAsync(
                "GetTransactionForCategory",
                new { UserId = requestModel.UserId, Year = requestModel.Year, Month = requestModel.Month, 
                    CategoryId = requestModel.CategoryId, TransactionType = requestModel.TransactionType },
                commandType: CommandType.StoredProcedure
            );

            var result= (await multi.ReadAsync<TransactionForCategory>()).ToList();

            response.DataList = new List<TransactionForCategoryResponseModel>
            {
                new TransactionForCategoryResponseModel{transactionForCategoriesYearOrMonth = result}
            };
            response.Message = "Successfully retrieved transaction category.";
            response.Code = StatusCodes.Status200OK;
            return response;
        }
    }
}
