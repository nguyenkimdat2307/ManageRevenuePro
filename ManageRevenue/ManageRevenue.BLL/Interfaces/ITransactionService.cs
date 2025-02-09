using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.BLL.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int month, int year);
        Task<Response<string>> AddTransactionSummary(TransactionViewModel transactionViewModel);
        Task<Response<string>> UpdateTransactionSummary(TransactionViewModel transactionViewModel);
        Task<Response<string>> DeleteTransactionSummary(int transactionId);
        Task<Response<TransactionDetailViewModel>> GetTransactionById(int transactionId);

    }
}
