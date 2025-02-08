using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.BLL.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int userId, int month, int year);
        Task<Response<string>> AddTransactionSummary(TransactionViewModel transactionViewModel);
    }
}
