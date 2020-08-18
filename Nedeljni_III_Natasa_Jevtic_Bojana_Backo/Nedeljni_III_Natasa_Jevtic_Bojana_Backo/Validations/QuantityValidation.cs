using System;
using System.Globalization;
using System.Windows.Controls;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Validations
{
    class QuantityValidation : ValidationRule
    {
        /// <summary>
        /// This method checks if user enter a positive number for quantity.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>ValidationResult.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value as string;
            if (!Decimal.TryParse(number, out decimal quantity))
            {
                return new ValidationResult(false, "Please enter a number.");
            }
            else if (quantity <= 0)
            {
                return new ValidationResult(false, "Please enter a positive number.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
