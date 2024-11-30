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
        int houseNo;
        int zipCode;
        int zone;
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

            // Declare variables to hold the converted values
            int houseNo;
            int zipCode;
            int zone;

            // Try to parse the values from the text boxes into integers
            if (!int.TryParse(HouseNoTxtbox.Text, out houseNo))
            {
                MessageBox.Show("Please enter a valid number for House No.");
                return; // Stop further execution if conversion fails
            }

            if (!int.TryParse(ZipCodeTxtbox.Text, out zipCode))
            {
                MessageBox.Show("Please enter a valid number for Zip Code.");
                return; // Stop further execution if conversion fails
            }

            if (!int.TryParse(ZoneTxtbox.Text, out zone))
            {
                MessageBox.Show("Please enter a valid number for Zone.");
                return; // Stop further execution if conversion fails
            }

            // Create a new User object with all fields, including address components
            User newUser = new User
            {
                FirstName = FirstnameTxtbox.Text,
                LastName = LastNameTxtbox.Text,
                MiddleName = MiddleNameTxtbox.Text,
                Suffix = SuffixTxtbox.Text,
                Bdate = BdayDateTimePicker.Value,
                Sex = SexCombobox.SelectedItem?.ToString(),
                Username = UsernameTxtbox.Text,
                Password = PasswordTxtbox.Text,
                ConfirmPassword = ConfirmPasswordTxtbox.Text,
                ContactNumber = ContactNumberTxtbox.Text,

                // New Address Components (use the parsed integer values)
                HouseNo = houseNo,
                ZipCode = zipCode,
                Zone = zone,
                Street = StreetTxtbox.Text,
                Barangay = BarangayTxtbox.Text,
                City = CityTxtbox.Text
            };

            // Validate the password and confirm password match
            if (newUser.Password != newUser.ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                // Save the new user to the database
                Database db = new Database();
                db.PatientReg1(newUser);

                // Open the PatientBasicInformationForm and pass relevant data
                var patientInfoForm = new PatientBasicInformationForm(newUser.Username, newUser.FirstName, newUser.LastName);
                patientInfoForm.Show();

                // Hide the current form
                this.Hide();
            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}