using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;
using ManageRevenue.Domain.Models.Transaction;

namespace ManageRevenue.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Response<string>> AddTransactionRevenue(TransactionViewModel transactionViewModel);
        Task<Response<string>> UpdateTransactionRevenue(TransactionViewModel transactionViewModel);
        Task<Response<string>> DeleteTransactionRevenue(int transactionId, int userId);
        Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int userId, int month, int year);
        Task<Response<TransactionDetailViewModel>> GetTransactionById(int transactionId);
        Task<Response<TransactionStatisticsSummaryViewModel>> GetTransactionStatisticsSummary(int userId, int year, int month);
        Task<Response<TransactionForCategoryResponseModel>> GetTransactionForCategorySummary(TransactionForCategoryRequestModel requestModel);
    }
}
