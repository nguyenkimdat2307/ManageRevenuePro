using ManageRevenue.BLL.Common;
using ManageRevenue.BLL.Services;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using ManageRevenue.UnitTest.Mocks;
using ManageRevenue.UnitTest.Mocks.Transaction;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ManageRevenue.UnitTest.Services.Transaction
{
    public class TransactionServiceTest
    {
        private readonly TransactionService _transactionService;
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<ISessionInfo> _sessionInfoMock;

        public TransactionServiceTest()
        {
            _transactionRepositoryMock = TransactionRepositoryMock.GetTransactionRepository();
            _sessionInfoMock = SessionInfoMock.GetSessionInfo();
            _transactionService = new TransactionService(_transactionRepositoryMock.Object, _sessionInfoMock.Object);
        }

        [Test]
        public async Task AddTransactionSummary_ShouldReturnSuccess()
        {
            // Arrange
            var transactionViewModel = new TransactionViewModel
            {
                CategoryId = 1,
                Amount = 1000,
                Date = System.DateTime.UtcNow,
                Description = "Test Transaction",
                TransactionType = 1
            };

            // Act
            var result = await _transactionService.AddTransactionSummary(transactionViewModel);

            // Assert
            Assert.That(result.Code, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(result.Message, Is.EqualTo("Transaction Added Successfully"));
        }

        [Test]
        public async Task DeleteTransactionSummary_ShouldReturnSuccess()
        {
            // Arrange
            int transactionId = 1;

            // Act
            var result = await _transactionService.DeleteTransactionSummary(transactionId);

            // Assert
            Assert.That(result.Code, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(result.Message, Is.EqualTo("Transaction Deleted Successfully"));
        }
        [Test]
        public async Task GetTransactionById_ShouldReturnTransactionDetails()
        {
            // Arrange
            int transactionId = 1;
            var expectedTransaction = new TransactionDetailViewModel
            {
                Id = 1,
                UserId = 1,
                CategoryId = 2,
                CategoryName = "Food",
                Amount = 500,
                Description = "Lunch",
                Date = new DateTime(2025, 3, 1),
            };
            // Act
            var result = await _transactionService.GetTransactionById(transactionId);

            // Assert
            Assert.That(result.Data.Id, Is.EqualTo(expectedTransaction.Id));
            Assert.That(result.Data.UserId, Is.EqualTo(expectedTransaction.UserId));
            Assert.That(result.Data.Amount, Is.EqualTo(expectedTransaction.Amount));
            Assert.That(result.Data.CategoryName, Is.EqualTo(expectedTransaction.CategoryName));
            Assert.That(result.Data.Description, Is.EqualTo(expectedTransaction.Description));
            Assert.That(result.Data.Date, Is.EqualTo(expectedTransaction.Date));
        }

    }
}
