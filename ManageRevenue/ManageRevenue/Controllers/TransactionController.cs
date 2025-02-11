using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Models;
using ManageRevenue.Models.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageRevenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMonthlyTransaction(int month, int year)
        {
            var result = await _transactionService.GetMonthlyTransactionSummary(month, year);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("new-transaction")]
        public async Task<IActionResult> AddTransaction(TransactionRequestModel transactionRequestModel)
        {
            var request = new TransactionViewModel
            {
                Amount = transactionRequestModel.Amount,
                CategoryId = transactionRequestModel.CategoryId,
                Date = transactionRequestModel.Date,
                Description = transactionRequestModel.Description,
                TransactionType = transactionRequestModel.TransactionType,
            };
            var result = await _transactionService.AddTransactionSummary(request);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("update-transaction")]
        public async Task<IActionResult> UpdateTransaction(UpdateTransactionRequestModel transactionRequestModel)
        {
            var request = new TransactionViewModel
            {
                Id = transactionRequestModel.TransactionId,
                NewAmount = transactionRequestModel.NewAmount,
                CategoryId = transactionRequestModel.CategoryId,
                Date = transactionRequestModel.NewDate,
                Description = transactionRequestModel.Description,
                TransactionType = transactionRequestModel.TransactionType,
            };
            var result = await _transactionService.UpdateTransactionSummary(request);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("delete-transaction")]
        public async Task<IActionResult> DeleteTransaction(int transactionId)
        {
            var result = await _transactionService.DeleteTransactionSummary(transactionId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("get-by-id-transaction")]
        public async Task<IActionResult> GetByIdTransaction(int transactionId)
        {
            var result = await _transactionService.GetTransactionById(transactionId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("get-transaction-statistics")]
        public async Task<IActionResult> GetTransactionStatis(int year)
        {
            var result = await _transactionService.GetTransactionStatisticsSummary(year);
            return Ok(result);
        }
    }
}
