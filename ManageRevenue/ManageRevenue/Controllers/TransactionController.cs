using ManageRevenue.BLL.Interfaces;
using ManageRevenue.Domain.Models;
using ManageRevenue.Models.Transaction;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public async Task<IActionResult> GetMonthlyTransaction(int userId, int month, int year)
        {
            var result = await _transactionService.GetMonthlyTransactionSummary(userId, month, year);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionRequestModel transactionRequestModel)
        {
            var request = new TransactionViewModel
            {
                UserId = transactionRequestModel.UserId,
                Amount = transactionRequestModel.Amount,
                CategoryId = transactionRequestModel.CategoryId,
                Date = transactionRequestModel.Date,
                Description = transactionRequestModel.Description,
                TransactionType = transactionRequestModel.TransactionType,
            };
            var result = await _transactionService.AddTransactionSummary(request);
            return Ok(result);
        }
    }
}
