using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Response<string>> AddTransactionSummary(TransactionViewModel transactionViewModel)
        {
            var result = await _transactionRepository.AddTransactionRevenue(transactionViewModel);
            return result;
        }

        public async Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int userId, int month, int year)
        {
            var result = await _transactionRepository.GetMonthlyTransactionSummary(userId, month, year);
            return result;
        }
    }
}
