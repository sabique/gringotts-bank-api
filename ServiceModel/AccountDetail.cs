using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    public class AccountDetail
    {
        public int AccountNumber { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Balance { get; set; }
    }
}
