namespace ManageRevenue.Domain.Models
{
    public class TransactionSummaryViewModel
    {
        public TransactionSummaryData Data { get; set; }

        public List<TransactionDetail> DataList { get; set; }

        public string Message { get; set; }
        public int Code { get; set; }
    }

    public class TransactionSummaryData
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal RemainingBalance { get; set; }
    }

   
 public class TransactionDetail
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string TransactionTypeDescription { get; set; }
    }
}
