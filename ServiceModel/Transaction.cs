﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Enum;

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
