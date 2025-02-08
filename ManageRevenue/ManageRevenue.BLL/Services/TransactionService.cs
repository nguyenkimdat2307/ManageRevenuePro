using ManageRevenue.BLL.Common;
using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ISessionInfo _sessionInfo;
        public TransactionService(ITransactionRepository transactionRepository, ISessionInfo sessionInfo)
        {
            _transactionRepository = transactionRepository;
            _sessionInfo = sessionInfo;
        }

        public async Task<Response<string>> AddTransactionSummary(TransactionViewModel transactionViewModel)
        {
            int userId = _sessionInfo.GetUserId();
            var param = new TransactionViewModel
            {
                CategoryId = transactionViewModel.CategoryId,
                Amount = transactionViewModel.Amount,
                UserId = userId,
                Date = transactionViewModel.Date,
                Description = transactionViewModel.Description,
                TransactionType = transactionViewModel.TransactionType
            };
            var result = await _transactionRepository.AddTransactionRevenue(param);
            return result;
        }

        public async Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int month, int year)
        {
            int userId = _sessionInfo.GetUserId();
            var result = await _transactionRepository.GetMonthlyTransactionSummary(userId, month, year);
            return result;
        }
    }
}
