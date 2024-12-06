using Guna.UI2.WinForms;
using Infocare_Project.NewFolder;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Infocare_Project
{
    public partial class PatientRegisterForm : Form
    {
        private PlaceHolderHandler _placeHolderHandler;
        int houseNo;
        int zipCode;
        int zone;
        public PatientRegisterForm()
        {
            InitializeComponent();
            _placeHolderHandler = new PlaceHolderHandler();
        }

        private void PatientRegisterForm_Load(object sender, EventArgs e)
        {
            BdayDateTimePicker.MaxDate = DateTime.Today;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure to cancel registration?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            Guna2TextBox[] requiredTextBoxes = {
                FirstnameTxtbox, LastNameTxtbox, MiddleNameTxtbox, SuffixTxtbox, CityTxtbox,
                ContactNumberTxtbox, ZipCodeTxtbox, ZoneTxtbox, StreetTxtbox, BarangayTxtbox, UsernameTxtbox
            };

            if (!InputValidator.ValidateAllFieldsFilled(requiredTextBoxes, "Please fill out all fields."))
            {
                return;
            }

            string contactNumber = ContactNumberTxtbox.Text;

            if (contactNumber.Length > 0 && (contactNumber.Length != 11 || !contactNumber.StartsWith("09") || !contactNumber.All(char.IsDigit)))
            {
                MessageBox.Show("Invalid number. The contact number must start with '09' and be exactly 11 digits.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!FirstnameTxtbox.Text.All(char.IsLetter) && !string.IsNullOrEmpty(FirstnameTxtbox.Text))
            {
                MessageBox.Show("First name must contain only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!MiddleNameTxtbox.Text.All(char.IsLetter) && !string.IsNullOrEmpty(MiddleNameTxtbox.Text))
            {
                MessageBox.Show("Middle name must contain only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!LastNameTxtbox.Text.All(char.IsLetter) && !string.IsNullOrEmpty(LastNameTxtbox.Text))
            {
                MessageBox.Show("Last name must contain only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!InputValidator.ValidateAlphabetic(FirstnameTxtbox, "First name must contain only letters. ex. (Juan)") ||
                !InputValidator.ValidateAlphabetic(LastNameTxtbox, "Last name must contain only letters. ex. (Dela Cruz)") ||
                !InputValidator.ValidateAlphabetic(CityTxtbox, "City must contain only letters. ex. (Caloocan)"))
            {
                return;
            }

            if (!InputValidator.ValidateNumeric(ContactNumberTxtbox, "Contact number must contain only numbers. ex.(09777864220)") ||
                !InputValidator.ValidateNumeric(ZipCodeTxtbox, "Zip Code must contain only numbers. ex. (1400)") ||
                !InputValidator.ValidateNumeric(ZoneTxtbox, "Zone must contain only numbers. ex. (1)"))
            {
                return;
            }

            int zipCode;
            int zone;

            if (!int.TryParse(ZipCodeTxtbox.Text, out zipCode))
            {
                MessageBox.Show("Please enter a valid number for Zip Code.");
                return;
            }

            if (!int.TryParse(ZoneTxtbox.Text, out zone))
            {
                MessageBox.Show("Please enter a valid number for Zone.");
                return;
            }
            Database db = new Database();
            if (db.IsUsernameExists(UsernameTxtbox.Text))
            {
                MessageBox.Show("The username is already in use. Please choose a different username.");
                return;
            }


            User newUser = new User
            {
                FirstName = FirstnameTxtbox.Text,
                LastName = LastNameTxtbox.Text,
                MiddleName = MiddleNameTxtbox.Text,
                Suffix = SuffixTxtbox.Text,
                Bdate = BdayDateTimePicker.Value,
                Sex = SexCombobox.SelectedItem?.ToString(),
                Username = UsernameTxtbox.Text,
                ContactNumber = ContactNumberTxtbox.Text,

                HouseNo = houseNo,
                ZipCode = zipCode,
                Zone = zone,
                Street = StreetTxtbox.Text,
                Barangay = BarangayTxtbox.Text,
                City = CityTxtbox.Text
            };

            if (newUser.Password != newUser.ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                Database Db = new Database();
                Db.PatientReg1(newUser);

                var patientInfoForm = new PatientBasicInformationForm(newUser.Username, newUser.FirstName, newUser.LastName);
                patientInfoForm.Show();

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        //TEXTBOX PLACE HOLDER BRO!!!

        private void UsernameTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(UsernameTxtbox, UserNameLabel, "Email");
        }

        private void ContactNumberTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ContactNumberTxtbox, ContactNumberLabel, "Contact Number");

            
        }


        private void FirstnameTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(FirstnameTxtbox, FirstNameLabel, "First name");
        }

        private void LastNameTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(LastNameTxtbox, LastNameLabel, "Last name");
        }

        private void HouseNoTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(HouseNoTxtbox, HouseLabel, "House No.");
        }

        private void ZipCodeTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ZipCodeTxtbox, ZipCodeLabel, "Zip Code");
        }

        private void ZoneTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ZoneTxtbox, ZoneLabel, "Zone");
        }

        private void MiddleNameTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(MiddleNameTxtbox, MiddleNameLabel, "Middle name");
        }

        private void SuffixTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(SuffixTxtbox, SuffixLabel, "Suffix");
        }

        private void StreetTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(StreetTxtbox, StreetLabel, "Street");
        }

        private void BarangayTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(BarangayTxtbox, BarangayLabel, "Barangay");
        }

        private void CityTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(CityTxtbox, CityLabel, "City");
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = {
                                    UsernameTxtbox, FirstnameTxtbox, LastNameTxtbox, MiddleNameTxtbox, SuffixTxtbox, CityTxtbox,
                                    ContactNumberTxtbox, ZipCodeTxtbox, ZoneTxtbox, StreetTxtbox, BarangayTxtbox
                                  };

            if (textBoxes.All(tb => string.IsNullOrWhiteSpace(tb.Text)))
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to go back?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    StaffLogin patientLoginForm = new StaffLogin();
                    patientLoginForm.Show();
                    this.Hide();
                }
            }
            else if (textBoxes.Any(tb => !string.IsNullOrWhiteSpace(tb.Text)))
            {
                DialogResult confirm = MessageBox.Show("Some fields are filled. Are you sure you want to go back? Unsaved changes may be lost.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    StaffLogin patientLoginForm = new StaffLogin();
                    patientLoginForm.Show();
                    this.Hide();

                }
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}