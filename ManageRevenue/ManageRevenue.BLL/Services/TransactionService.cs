using ManageRevenue.BLL.Common;
using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using System.Transactions;

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

        public async Task<Response<string>> DeleteTransactionSummary(int transactionId)
        {
            int userId = _sessionInfo.GetUserId();
            var result = await _transactionRepository.DeleteTransactionRevenue(transactionId,userId);
            return result;
        }

        public async Task<TransactionSummaryViewModel> GetMonthlyTransactionSummary(int month, int year)
        {
            int userId = _sessionInfo.GetUserId();
            var result = await _transactionRepository.GetMonthlyTransactionSummary(userId, month, year);
            return result;
        }

        public async Task<Response<TransactionDetailViewModel>> GetTransactionById(int transactionId)
        {
            var result = await _transactionRepository.GetTransactionById(transactionId);
            return result;
        }

        public async Task<Response<TransactionStatisticsSummaryViewModel>> GetTransactionStatisticsSummary(int year)
        {
            int userId = _sessionInfo.GetUserId();
            var result = await _transactionRepository.GetTransactionStatisticsSummary(userId,year);
            return result;
        }

        public async Task<Response<string>> UpdateTransactionSummary(TransactionViewModel transactionViewModel)
        {
            int userId = _sessionInfo.GetUserId();

            var param = new TransactionViewModel
            {
                Id = transactionViewModel.Id,
                CategoryId = transactionViewModel.CategoryId,
                UserId = userId,
                Date = transactionViewModel.Date,
                Description = transactionViewModel.Description,
                TransactionType = transactionViewModel.TransactionType,
                NewAmount = transactionViewModel.NewAmount
            };
            var result = await _transactionRepository.UpdateTransactionRevenue(param);
            return result;
        }
    }
}
