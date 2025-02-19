namespace ManageRevenue.Domain.Models.Transaction
{
    public class TransactionForCategoryResponseModel
    {
        public List<TransactionForCategory> transactionForCategoriesYearOrMonth { get; set; }
    }
    public class TransactionForCategory
    {
        public int Month { get; set; }
        public DateTime Day { get; set; }
        public decimal TotalAmount { get; set; }
        public string? CategoryName { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; } 
    }
}
