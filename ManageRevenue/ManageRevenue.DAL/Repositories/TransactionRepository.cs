using Dapper;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Data;

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
                TransactionType = transactionViewModel.TransactionType
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


    }
}
