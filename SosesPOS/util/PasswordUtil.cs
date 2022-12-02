using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SosesPOS.util
{
    internal class PasswordUtil
    {
        public static bool isPasswordValid(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return false;
            }
            //Regex hasNumber = new Regex(@"[0-9]+");
            //Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasMinimum6Chars = new Regex(@"^.{6,20}$", RegexOptions.Compiled);
            return hasMinimum6Chars.IsMatch(password);
        }

        public static bool isPasswordEqual(string newPassword, string cNewPassword)
        {
            if (String.IsNullOrWhiteSpace(newPassword) || String.IsNullOrWhiteSpace(cNewPassword))
            {
                return false;
            }

            return newPassword.Equals(cNewPassword);
        }
    }
}
