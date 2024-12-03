using Guna.UI2.WinForms;
using Infocare_Project;
using Infocare_Project_1.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            Staff newStaff = new Staff
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextbox.Text,
                MiddleName = MiddleNameTextbox.Text,
                Email = EmailTextbox.Text,
                ContactNumber = ConatactNumberTextbox.Text,
                Username = UserNameTextBox.Text,
                Password = PasswordTextBox.Text,
                ConfirmPassword = ConfirmPasswordTextBox.Text,
            };

            if (newStaff.Password != newStaff.ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                Database db = new Database();
                db.AddStaff(newStaff);
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
    }
}