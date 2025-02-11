using System.Numerics;

namespace ManageRevenue.Domain.Models
{
    public class TransactionStatisticsSummaryViewModel
    {
        
        public TransactionStatistics TotalStatistics { get; set; }
        public List<TransactionStatisticsSpendandCollect> listSpend { get; set; }
        public List<TransactionStatisticsSpendandCollect> listCollect { get; set; }
    }
    public class TransactionStatistics
    {
        public decimal TotalSpend { get; set; } 
        public decimal TotalCollect { get; set; }
    }

    public class TransactionStatisticsSpendandCollect
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public double Percentage { get; set; }
    }
}
