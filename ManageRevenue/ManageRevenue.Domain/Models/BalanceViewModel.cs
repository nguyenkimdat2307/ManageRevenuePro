namespace ManageRevenue.Domain.Models
{
    public class Balance
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
