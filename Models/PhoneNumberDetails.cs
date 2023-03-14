using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneValidatorAPI.Models
{
    public class PhoneNumberDetails
    {
        public bool IsValid { get; set; }
        public bool IsPossible { get; set; }
        public string PhoneType { get; set; }
        public string InternationalFormat { get; set; }


    }
}