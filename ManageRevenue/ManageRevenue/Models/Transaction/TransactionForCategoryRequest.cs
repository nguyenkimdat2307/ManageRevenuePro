namespace ManageRevenue.Models.Transaction
{
    public class TransactionForCategoryRequest
    {
        public int CategoryId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int TransactionType { get; set; }
    }
}
