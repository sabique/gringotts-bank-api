using System;
using System.ComponentModel.DataAnnotations;
using static ServiceModel.Enum;

namespace ServiceModel
{
    public class Account
    {
        [Required]
        [EnumDataType(typeof(AccountType), ErrorMessage = "Please select among following values - 0 for Saving, 1 for Current.")]
        public AccountType Type { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Minimum USD 1 is required to open an account")]
        public decimal Amount { get; set; }
    }
}
