using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Enum;

namespace ServiceModel
{
    public class OpenAccount : Account
    {
        public int CustomerId { get; set; }
    }
}
