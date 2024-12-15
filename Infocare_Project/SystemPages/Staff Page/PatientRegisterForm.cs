using Guna.UI2.WinForms;
using Infocare_Project.NewFolder;
using Infocare_Project_1;
using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models;
using Microsoft.AspNetCore.SignalR.Protocol;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Infocare_Project
{
    public partial class PatientRegisterForm : Form
    {
        bool passShow = true;
        private PlaceHolderHandler _placeHolderHandler;
        public Action ReloadResults;
        int houseNo;
        int zipCode;
        int zone;
        ModalMode mode;
        int AccountID;

        PatientModel extractedInfo;
        public PatientRegisterForm(ModalMode mode, int AccountId = 0)
        {
            InitializeComponent();
            this.mode = mode;
            this.AccountID = AccountId;
            _placeHolderHandler = new PlaceHolderHandler();
            PageTitle.Text = mode == ModalMode.Edit ? "Patient Information" : "Patient Registration";

            if (mode == ModalMode.Edit)
            {
                DeleteBtn.Visible = true;
            }
        }

        private void PatientRegisterForm_Load(object sender, EventArgs e)
        {
            if (mode == ModalMode.Add)
            {
                BdayDateTimePicker.MaxDate = DateTime.Today;
            }
            else
            {
                //Edit
                //extractedInfo = Database.GetPatientInfo(AccountID);
                extractedInfo.AccountID = AccountID;
                FillUpFields(extractedInfo);
            }

        }

        public void FillUpFields(PatientModel info)
        {
            FirstnameTxtbox.Text = info.FirstName;
            LastNameTxtbox.Text = info.LastName;
            MiddleNameTxtbox.Text = info.MiddleName;
            SuffixTxtbox.Text = info.Suffix;
            EmailTxtbox.Text = info.Email;
            ContactNumberTxtbox.Text = info.ContactNumber.ToString();

            BdayDateTimePicker.Value = info.BirthDate;

            SexCombobox.SelectedItem = info.sex;
            //return $"{HouseNo},{ZipCode}, {Zone}, {Street} street, Brgy. {Barangay}, {City}";


            HouseNoTxtbox.Text = info.Address.HouseNo.ToString();
            ZipCodeTxtbox.Text = info.Address.ZipCode.ToString();
            ZoneTxtbox.Text = info.Address.Zone.ToString();
            StreetTxtbox.Text = info.Address.Street;
            BarangayTxtbox.Text = info.Address.Barangay;
            CityTxtbox.Text = info.Address.City;
        }

        public PatientModel SetupObj()
        {
            PatientModel patient = new PatientModel()
            {
                FirstName = FirstnameTxtbox.Text,
                LastName = LastNameTxtbox.Text,
                UserName = UsernameTextbox.Text,
                Password = PasswordTextBox.Text,
                MiddleName = MiddleNameTxtbox.Text,
                BirthDate = BdayDateTimePicker.Value,
                Suffix = SuffixTxtbox.Text,
                Email = EmailTxtbox.Text,
                ContactNumber = ContactNumberTxtbox.Text,
                sex = SexCombobox.SelectedItem.ToString()
            };

            AddressModel address = new AddressModel()
            {
                HouseNo = int.Parse(HouseNoTxtbox.Text),
                ZipCode = int.Parse(ZipCodeTxtbox.Text),
                City = CityTxtbox.Text,
                Zone = int.Parse(ZoneTxtbox.Text),
                Street = StreetTxtbox.Text,
                Barangay = BarangayTxtbox.Text,
            };

            patient.Address = address;


            return patient;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {

            DialogResult confirm = MessageBox.Show($"Are you sure to cancel {(mode == ModalMode.Add ? "cancel registration" : "close")}?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }
        private void EnterButton_Click(object sender, EventArgs e)
        {
            string contactNumber = ContactNumberTxtbox.Text;

            if (contactNumber.Length > 0 && (contactNumber.Length != 11 || !contactNumber.StartsWith("09") || !contactNumber.All(char.IsDigit)))
            {
                MessageBox.Show("Invalid number. The contact number must start with '09' and be exactly 11 digits.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!FirstnameTxtbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(FirstnameTxtbox.Text))
            {
                MessageBox.Show("First name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!MiddleNameTxtbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(MiddleNameTxtbox.Text))
            {
                MessageBox.Show("Middle name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!LastNameTxtbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(LastNameTxtbox.Text))
            {
                MessageBox.Show("Last name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Database.UsernameExists(UsernameTextbox.Text, Role.Patient) && mode == ModalMode.Add)
            {
                MessageBox.Show("The username is already in use. Please choose a different username.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string password = PasswordTextBox.Text;

            if (mode == ModalMode.Add)
            {
                if (!ProcessMethods.ValidatePassword(password))
                {
                    return;
                }
            }


            if (!InputValidator.ValidateAlphabetic(FirstnameTxtbox, "First name must contain only letters. ex. (Juan)") ||
                !InputValidator.ValidateAlphabetic(LastNameTxtbox, "Last name must contain only letters. ex. (Dela Cruz)") ||
                !InputValidator.ValidateAlphabetic(CityTxtbox, "City must contain only letters. ex. (Caloocan)"))
            {
                return;
            }

            if (!UsernameTextbox.Text.All(char.IsLetter) && !string.IsNullOrEmpty(UsernameTextbox.Text))
            {
                MessageBox.Show("Username must contain only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!InputValidator.ValidateNumeric(ContactNumberTxtbox, "Contact number must contain only numbers. ex.(09777864220)") ||
                !InputValidator.ValidateNumeric(ZipCodeTxtbox, "Zip Code must contain only numbers. ex. (1400)") ||
                !InputValidator.ValidateNumeric(ZoneTxtbox, "Zone must contain only numbers. ex. (1)"))
            {
                return;
            }




            string[] validSuffixes = { "Jr.", "Sr.", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "Jr", "Sr", "N/A" };

            string enteredText = SuffixTxtbox.Text.Trim();

            if (!string.IsNullOrEmpty(enteredText) && !validSuffixes.Any(suffix => string.Equals(suffix, enteredText, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Please enter a valid suffix (e.g., Jr., Sr., I, II, III, IV, etc.).", "Invalid Suffix", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ZipCode;
            int Zone;

            if (!int.TryParse(ZipCodeTxtbox.Text, out ZipCode))
            {
                MessageBox.Show("Please enter a valid number for Zip Code.");
                return;
            }

            if (!int.TryParse(ZoneTxtbox.Text, out Zone))
            {
                MessageBox.Show("Please enter a valid number for Zone.");
                return;
            }

            if (Database.IsUsernameExists(EmailTxtbox.Text))
            {
                MessageBox.Show("The username is already in use. Please choose a different username.");
                return;
            }

            Guna2TextBox[] requiredTextBoxes = {
                FirstnameTxtbox, LastNameTxtbox, MiddleNameTxtbox, SuffixTxtbox, CityTxtbox,
                ContactNumberTxtbox, ZipCodeTxtbox, ZoneTxtbox, StreetTxtbox, BarangayTxtbox, EmailTxtbox
            };

            if (!InputValidator.ValidateAllFieldsFilled(requiredTextBoxes, "Please fill out all fields."))
            {
                return;
            }

            AddressModel address = new AddressModel()
            {
                HouseNo = houseNo,
                Street = StreetTxtbox.Text,
                Barangay = BarangayTxtbox.Text,
                City = CityTxtbox.Text,
                ZipCode = zipCode,
                Zone = zone
            };

            PatientModel newPatient = new PatientModel
            {
                UserName = UsernameTextbox.Text,
                FirstName = FirstnameTxtbox.Text,
                LastName = LastNameTxtbox.Text,
                MiddleName = MiddleNameTxtbox.Text,
                Password = PasswordTextBox.Text,
                ContactNumber = ContactNumberTxtbox.Text,
                Email = EmailTxtbox.Text,
                Address = address,
                sex = SexCombobox.SelectedItem.ToString()
            };
            
            
            this.Cursor = Cursors.WaitCursor;
            PatientModel editedInfo = SetupObj();
            PatientModel ProperModel = mode == ModalMode.Add ? newPatient : extractedInfo;

            Database.PatientRegFunc(editedInfo, mode);

            this.Cursor = Cursors.Default;
            var patientInfoForm = new PatientBasicInformationForm(ProperModel, mode);
            patientInfoForm.ReloadResults += ReloadResults;
            patientInfoForm.DeletePatientAndReload += DeletePatientAndReload;
            patientInfoForm.TopMost = true;
            patientInfoForm.Show();
            this.Hide();


        }

        private void UsernameTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(EmailTxtbox, EmailLabel, "Email");
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
                                    EmailTxtbox, FirstnameTxtbox, LastNameTxtbox, MiddleNameTxtbox, SuffixTxtbox, CityTxtbox,
                                    ContactNumberTxtbox, ZipCodeTxtbox, ZoneTxtbox, StreetTxtbox, BarangayTxtbox
                                  };

            if (textBoxes.All(tb => string.IsNullOrWhiteSpace(tb.Text)))
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to go back?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
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

        private void DeletePatientAndReload()
        {


            Database.DeletePatientByUsername(extractedInfo.UserName);
            ReloadResults.Invoke();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult isDelete = MessageBox.Show("Are you sure you want to delete this information?", "Patient Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (isDelete == DialogResult.No) return;
            DeletePatientAndReload();
            this.Hide();

        }

        private void PasswordTextBox_IconRightClick(object sender, EventArgs e)
        {
            if (passShow)
            {
                PasswordTextBox.PasswordChar = '\0';
                PasswordTextBox.IconRight = AdminDoctor_Panel.Properties.Resources.hide_password_logo;
                passShow = false;
            }
            else
            {
                PasswordTextBox.PasswordChar = '●';
                PasswordTextBox.IconRight = AdminDoctor_Panel.Properties.Resources.show_password_logo;
                passShow = true;
            }
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text.Trim() == "")
            {
                passValidatorMsg.Visible = false;
            } else
            {
                passValidatorMsg.Visible = true;
                string msg =
                !Regex.IsMatch(PasswordTextBox.Text, @"[A-Z]") ? "Add at least one uppercase letter" :
                !Regex.IsMatch(PasswordTextBox.Text, @"[^a-zA-Z0-9\s]") ? "Add At least one special character" : !Regex.IsMatch(PasswordTextBox.Text, @"[\d]") ? "Add At least one number" : !Regex.IsMatch(PasswordTextBox.Text, @".{8,}") ? "Must have at least 8 characters long" : "";
                
                if (msg == "")
                {

                    passValidatorMsg.Text = "*Strong Enough";
                    passValidatorMsg.ForeColor = Color.Green;
                }
                else
                {
                    passValidatorMsg.Text = "*" + msg;
                    passValidatorMsg.ForeColor = Color.Red;

                }

            }
        }
    }
}