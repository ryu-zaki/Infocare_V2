using Guna.UI2.WinForms;
using Infocare_Project;
using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class AddStaff : Form
    {
        public AddStaff()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            string contactNumber = ConatactNumberTextbox.Text;

            if (contactNumber.Length > 0 && (contactNumber.Length != 11 || !contactNumber.StartsWith("09") || !contactNumber.All(char.IsDigit)))
            {
                MessageBox.Show("Invalid number. The contact number must start with '09' and be exactly 11 digits.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!FirstNameTextBox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("First name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            if (!UserNameTextBox.Text.All(char.IsLetter) && !string.IsNullOrEmpty(UserNameTextBox.Text))
            {
                MessageBox.Show("Username must contain only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Database.UsernameExistsStaff(UserNameTextBox.Text))
            {
                MessageBox.Show("The username is already in use. Please choose a different username.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string password = PasswordTextBox.Text;

            if (!ProcessMethods.ValidatePassword(password))
            {
                return;
            }

            if (!LastNameTextbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(LastNameTextbox.Text))
            {
                MessageBox.Show("Last name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            if (!ProcessMethods.ValidateEmail(EmailTextbox.Text))
            {
                MessageBox.Show("Please Enter a valid Email.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!MiddleNameTextbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(MiddleNameTextbox.Text))
            {
                MessageBox.Show("Middle name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            Guna2TextBox[] requiredTextBoxes = {
                FirstNameTextBox, LastNameTextbox, MiddleNameTextbox, UserNameTextBox, PasswordTextBox, ConfirmPasswordTextBox
            };

            if (!InputValidator.ValidateAllFieldsFilled(requiredTextBoxes, "Please fill out all fields."))
            {
                return;
            }

            if (!InputValidator.ValidateAlphabetic(FirstNameTextBox, "First name must contain only letters. ex. (Juan)") ||
                !InputValidator.ValidateAlphabetic(LastNameTextbox, "Last name must contain only letters. ex. (Dela Cruz)"))
            {
                return;
            }

            string[] validSuffixes = { "Jr.", "Sr.", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "Jr", "Sr", "N/A" };

            string enteredText = SuffixTextbox.Text.Trim();

            if (!string.IsNullOrEmpty(enteredText) && !validSuffixes.Any(suffix => string.Equals(suffix, enteredText, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Please enter a valid suffix (e.g., Jr., Sr., I, II, III, IV, etc.).", "Invalid Suffix", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StaffModel newStaff = new StaffModel
            {
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextbox.Text.Trim(),
                MiddleName = MiddleNameTextbox.Text,
                ContactNumber = ConatactNumberTextbox.Text,
                UserName = UserNameTextBox.Text,
                Password = PasswordTextBox.Text,
                Suffix = SuffixTextbox.Text,
                Email = EmailTextbox.Text
            };

            if (PasswordTextBox.Text.Trim() != ConfirmPasswordTextBox.Text.Trim())
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                Database.AddStaff(newStaff);
                MessageBox.Show("Staff added successfully!");
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AddStaff_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close? Unsaved changes will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back? Unsaved changes will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            

            Guna2TextBox? textBox = sender as Guna2TextBox;

            string msg =
                !Regex.IsMatch(textBox.Text, @"[A-Z]") ? "Must contain atleast one uppercase letter" :
                !Regex.IsMatch(textBox.Text, @"[^a-zA-Z0-9\s]") ? "Must have At least one special character" : !Regex.IsMatch(textBox.Text, @"[^a-zA-Z0-9\s]") ? "Must have At least one number" : !Regex.IsMatch(textBox.Text, @".{8,}") ? "Must be at least 8 characters long" :  "";
            passValidatorMsg.Visible = true;
            if (msg == "")
            {
               
                passValidatorMsg.Text = "Strong Enough";
                passValidatorMsg.ForeColor = Color.Green;
            }
            else
            {
                passValidatorMsg.Text = msg;
                passValidatorMsg.ForeColor = Color.Red;

            }


        }
    }
}