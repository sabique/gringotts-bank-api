using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Account
    {
        [Key]
        public long Id { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long CustomerId { get; set; }
    }
}
