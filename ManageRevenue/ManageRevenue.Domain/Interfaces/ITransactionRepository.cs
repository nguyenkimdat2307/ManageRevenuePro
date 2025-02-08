using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Response<string>> AddTransactionRevenue(TransactionViewModel transactionViewModel);
        Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int userId, int month, int year);
    }
}
