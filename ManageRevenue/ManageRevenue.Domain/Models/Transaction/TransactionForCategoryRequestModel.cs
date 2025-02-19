namespace ManageRevenue.Domain.Models.Transaction
{
    public class TransactionForCategoryRequestModel
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int TransactionType { get; set; }
    }
}
