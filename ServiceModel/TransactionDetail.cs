using System;

namespace ServiceModel
{
    public class TransactionDetail
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Balance { get; set; }
    }
}
