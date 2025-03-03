using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Interfaces;
using ManageRevenue.Domain.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ManageRevenue.UnitTest.Mocks.Transaction
{
    public class TransactionRepositoryMock
    {
        public static Mock<ITransactionRepository> GetTransactionRepository()
        {
            var mockRepo = new Mock<ITransactionRepository>();

            mockRepo.Setup(repo => repo.AddTransactionRevenue(It.IsAny<TransactionViewModel>()))
                .ReturnsAsync(new Response<string> { Code = StatusCodes.Status200OK, Message = "Transaction Added Successfully" });

            mockRepo.Setup(repo => repo.DeleteTransactionRevenue(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Response<string> { Code = StatusCodes.Status200OK, Message = "Transaction Deleted Successfully" });

            mockRepo.Setup(repo => repo.GetTransactionById(It.IsAny<int>()))
                .ReturnsAsync(new Response<TransactionDetailViewModel>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Transaction Retrieved Successfully",
                    Data = new TransactionDetailViewModel
                    {
                        Id = 1,
                        UserId = 1,
                        CategoryId = 2,
                        CategoryName = "Food",
                        Amount = 500,
                        Description = "Lunch",
                        Date = new DateTime(2025, 3, 1),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                });

            return mockRepo;
        }
    }
}
