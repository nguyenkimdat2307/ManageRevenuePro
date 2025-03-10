﻿namespace ManageRevenue.Models.Transaction
{
    public class TransactionRequestModel
    {
        public int CategoryId { get; set; }
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}
