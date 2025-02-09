namespace ManageRevenue.Models.Transaction
{
    public class UpdateTransactionRequestModel
    {
        public int TransactionId { get; set; }
        public int CategoryId { get; set; }
        public int TransactionType { get; set; }
        public decimal NewAmount { get; set; }
        public string? Description { get; set; }
        public DateTime NewDate { get; set; }
    }
}
