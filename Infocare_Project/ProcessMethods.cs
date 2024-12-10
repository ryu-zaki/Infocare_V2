using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1
{
    public static  class ProcessMethods
    {
        public static bool ValidatePassword(string password)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$");

            if (!regex.IsMatch(password))
            {
                MessageBox.Show("Password must be at least 8 characters long and include:\n- At least one uppercase letter\n- At least one lowercase letter\n- At least one number\n- At least one special character (e.g., @, !, etc.)",
                                "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public static bool IsValidTextInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            if (input.Equals("N/A", StringComparison.OrdinalIgnoreCase))
                return true;

            return input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }


        public static string HashCharacter(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);

                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
