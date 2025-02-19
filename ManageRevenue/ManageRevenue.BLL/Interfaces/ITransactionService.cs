using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;
using ManageRevenue.Domain.Models.Transaction;

namespace ManageRevenue.BLL.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int month, int year);
        Task<Response<string>> AddTransactionSummary(TransactionViewModel transactionViewModel);
        Task<Response<string>> UpdateTransactionSummary(TransactionViewModel transactionViewModel);
        Task<Response<string>> DeleteTransactionSummary(int transactionId);
        Task<Response<TransactionDetailViewModel>> GetTransactionById(int transactionId);
        Task<Response<TransactionStatisticsSummaryViewModel>> GetTransactionStatisticsSummary(int year,int month);
        Task<Response<TransactionForCategoryResponseModel>> GetTransactionForCategory(TransactionForCategoryRequestModel requestModel);

    }
}
