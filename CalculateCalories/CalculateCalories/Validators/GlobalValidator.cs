using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CalculateCalories.Validators
{
    class GlobalValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double d = 0;
            try
            {
                d = double.Parse((string)value);
            }
            catch
            {
                return new ValidationResult(false, "Некорректное значение");
            }

            return new ValidationResult(true, null);
        }
    }
}
