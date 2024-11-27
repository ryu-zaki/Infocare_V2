using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Infocare_Project
{
    public partial class PatientRegisterForm : Form
    {
        public PatientRegisterForm()
        {
            InitializeComponent();
        }

        private void PatientRegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            // Initialize User object
            User newUser = new User()
            {
                FirstName = FirstnameTxtbox.Text,
                LastName = LastNameTxtbox.Text,
                MiddleName = MiddleNameTxtbox.Text,
                Suffix = SuffixTxtbox.Text,
                Bdate = BdayDateTimePicker.Value,
                Sex = SexCombobox.SelectedItem.ToString(),
                Username = UsernameTxtbox.Text,
                Password = PasswordTxtbox.Text,
                ConfirmPassword = ConfirmPasswordTxtbox.Text,
                ContactNumber = ContactNumberTxtbox.Text
            };

            // Validate Passwords
            if (newUser.Password != newUser.ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Register the user (e.g., in the database)
            Database db = new Database();
            db.PatientReg1(newUser);  // Assuming PatientReg1 can handle a User object

            MessageBox.Show("Registration successful!");


        }
    }
}
