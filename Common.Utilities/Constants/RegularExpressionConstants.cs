using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities.Constants
{
    public static class RegularExpressionConstants
    {
        public const string NumericValidation = @"^\d+$";
        public const string CharacterValidation = @"[A-z^şŞıİçÇöÖüÜĞğ\s]*";
        public const string EmailValidation = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string PriceValidation = @"^[0-9]+\.?[0-9,]*";
    }
}
