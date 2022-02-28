using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public double Balance { get; set; }
    }
}
