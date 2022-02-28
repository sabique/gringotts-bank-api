using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    public class Transaction
    {
        [Range(1, int.MaxValue, ErrorMessage = "Minimum USD 1 is required to transact")]
        public decimal Amount { get; set; }
        [Range(100000, long.MaxValue, ErrorMessage = "Enter a valid account number")]
        public int AccountId { get; set; }
    }
}
