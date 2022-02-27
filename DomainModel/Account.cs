using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CustomerId { get; set; }
    }
}
