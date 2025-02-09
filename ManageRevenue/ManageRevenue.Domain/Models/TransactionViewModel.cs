namespace ManageRevenue.Domain.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public decimal NewAmount { get; set; }
        public string? Description { get; set; }
        public int TransactionType { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
