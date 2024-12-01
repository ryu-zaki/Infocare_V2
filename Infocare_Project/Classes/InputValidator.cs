using Guna.UI2.WinForms;
using System.Text.RegularExpressions;

namespace Infocare_Project_1.Classes
{
    internal static class InputValidator
    {
        public static bool IsNotEmpty(Guna2TextBox textBox)
        {
            return !string.IsNullOrWhiteSpace(textBox.Text);
        }

        public static bool IsAlphabetic(Guna2TextBox textBox)
        {
            string pattern = @"^[a-zA-Z\s]+$";
            return Regex.IsMatch(textBox.Text, pattern);
        }

        public static bool IsNumeric(Guna2TextBox textBox)
        {
            string pattern = @"^\d+$";
            return Regex.IsMatch(textBox.Text, pattern);
        }

        public static bool ValidateNotEmpty(Guna2TextBox textBox, string errorMessage)
        {
            if (!IsNotEmpty(textBox))
            {
                textBox.BorderColor = System.Drawing.Color.Red; 
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Focus();
                return false;
            }
            //textBox.BorderColor = System.Drawing.Color.Green; 
           return true;
        }

        public static bool ValidateAlphabetic(Guna2TextBox textBox, string errorMessage)
        {
            if (!IsAlphabetic(textBox))
            {
                textBox.BorderColor = System.Drawing.Color.Red; 
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Focus();
                return false;
            }
            textBox.BorderColor = System.Drawing.Color.LightBlue; 
            return true;
        }

        public static bool ValidateNumeric(Guna2TextBox textBox, string errorMessage)
        {
            if (!IsNumeric(textBox))
            {
                textBox.BorderColor = System.Drawing.Color.Red; 
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Focus();
                return false;
            }
            textBox.BorderColor = System.Drawing.Color.LightBlue; 
            return true;
        }

        public static bool ValidateAllFieldsFilled(Guna2TextBox[] textBoxes, string errorMessage)
        {
            foreach (var textBox in textBoxes)
            {
                if (!IsNotEmpty(textBox))
                {
                    MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Focus();
                    return false;
                }
            }
            return true;
        }
    }
}