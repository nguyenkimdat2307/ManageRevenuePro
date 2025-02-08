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
        [HttpPost]
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
    }
}
