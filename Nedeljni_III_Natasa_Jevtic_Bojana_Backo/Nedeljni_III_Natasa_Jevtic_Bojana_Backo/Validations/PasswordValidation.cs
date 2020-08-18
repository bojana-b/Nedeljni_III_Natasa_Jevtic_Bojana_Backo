using System;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Validations
{
    public static class PasswordValidation
    {
        /// <summary>
        /// This method checks if password have at least 5 characters
        /// </summary>
        public static bool PasswordOk(string password)
        {
            int minChar = 5;

            // check the entered password
            if (password.Length < minChar)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
