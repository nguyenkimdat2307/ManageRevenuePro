namespace ManageRevenue.Domain.Models
{
    public class TransactionSummaryViewModel
    {
        // Dữ liệu tổng thu nhập, chi tiêu và số dư
        public TransactionSummaryData Data { get; set; }

        // Dữ liệu chi tiết từng giao dịch
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
        public decimal Amount { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string TransactionTypeDescription { get; set; }
    }

}
