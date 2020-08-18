using System;
using System.Globalization;
using System.Windows.Controls;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Validations
{
    class NumberOfPersonsValidation : ValidationRule
    {
        /// <summary>
        /// This method checks if user enter a positive number for number of persons.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>ValidationResult.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value as string;
            if (!Int32.TryParse(number, out int numberOfPersons))
            {
                return new ValidationResult(false, "Please enter a number.");
            }
            else if (numberOfPersons <= 0)
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
