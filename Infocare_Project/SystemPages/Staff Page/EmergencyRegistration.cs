using Infocare_Project.NewFolder;
using Infocare_Project_1;
using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project
{
    public partial class EmergencyRegistration : Form
    {
        private PlaceHolderHandler _placeHolderHandler;
        public Action ReloadResults;
        public Action DeletePatientAndReload;
        PatientModel patient;
        ModalMode mode;
        PanelMode panelMode;
        public EmergencyRegistration(PatientModel patient, ModalMode mode, PanelMode panelMode)
        {
            InitializeComponent();
            this.mode = mode;

            this.panelMode = panelMode;
            _placeHolderHandler = new PlaceHolderHandler();
            NameLabel.Text = $"{patient.LastName}, {patient.FirstName}";
            this.patient = patient;

            if (mode == ModalMode.Edit)
            {
                DeleteBtn.Visible = panelMode == PanelMode.AdminDoc;
                RegisterButton.Text = "Update";
            } else
            {
                DeleteBtn.Visible = false;
                RegisterButton.Text = "Register";
            }
        }



        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult YesNO = MessageBox.Show("Are you sure to cancel registration?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (YesNO == DialogResult.Yes)
            {
                try
                {
                    Database.DeletePatientByUsername(patient.UserName);

                    MessageBox.Show("Your data has been deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting data: " + ex.Message);
                }
                finally
                {
                    this.Close();
                }
            }
        }

        private void EmergencyRegistration_Load(object sender, EventArgs e)
        {
            LoadPatientName();
        }

        private void LoadPatientName()
        {
            string fullName = Database.GetPatientName(patient);

            if (!string.IsNullOrEmpty(fullName))
            {
                NameLabel.Text = fullName;
            }
            else
            {
                NameLabel.Text = "No data found.";
            }
        }

        public EmergencyContactModel SetupInfo()
        {
            AddressModel addressInfo = new AddressModel()
            {
                HouseNo = int.Parse(HouseNoTxtbox.Text),
                ZipCode = int.Parse(ZipCodeTxtbox.Text),
                Zone = int.Parse(ZoneTxtbox.Text),
                Barangay = BarangayTxtbox.Text,
                City = CityTxtbox.Text,
                Street = StreetTxtbox.Text
            };


            EmergencyContactModel info = new EmergencyContactModel()
            {
                FirstName = FirstnameTxtbox.Text,
                LastName = LastNameTxtbox.Text,
                MiddleName = MiddleNameTxtbox.Text,
                Suffix = SuffixTxtbox.Text,
                address = addressInfo

            };

            return info;

        }

        private void RegisterButton_Click_1(object sender, EventArgs e)
        {
            //VALIDATION

            if (!FirstnameTxtbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(FirstnameTxtbox.Text))
            {
                MessageBox.Show("First name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            if (!LastNameTxtbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(LastNameTxtbox.Text))
            {
                MessageBox.Show("Last name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            if (!MiddleNameTxtbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '/' && !string.IsNullOrEmpty(MiddleNameTxtbox.Text)))
            {
                MessageBox.Show("Middle name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            string[] validSuffixes = { "Jr.", "Sr.", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "Jr", "Sr", "N/A" };

            string enteredText = SuffixTxtbox.Text.Trim();

            // Check if the entered text is one of the valid suffixes (case-insensitive)
            if (!string.IsNullOrEmpty(enteredText) && !validSuffixes.Any(suffix => string.Equals(suffix, enteredText, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Please enter a valid suffix (e.g., Jr., Sr., I, II, III, IV, etc.).", "Invalid Suffix", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int zipCode;
            int Zone;

            if (!int.TryParse(ZipCodeTxtbox.Text, out zipCode))
            {
                MessageBox.Show("Please enter a valid number for Zip Code.");
                return;
            }

            if (!int.TryParse(ZoneTxtbox.Text, out Zone))
            {
                MessageBox.Show("Please enter a valid number for Zone.");
                return;
            }

            try
            {
                string firstname = FirstnameTxtbox.Text.Trim();
                string middlename = MiddleNameTxtbox.Text.Trim();
                string lastname = LastNameTxtbox.Text.Trim();
                string suffix = SuffixTxtbox.Text.Trim();

                int housenum = string.IsNullOrWhiteSpace(HouseNoTxtbox.Text) ? 0 : Convert.ToInt32(HouseNoTxtbox.Text);
                string street = string.IsNullOrWhiteSpace(StreetTxtbox.Text) ? string.Empty : StreetTxtbox.Text.Trim();
                string barangay = string.IsNullOrWhiteSpace(BarangayTxtbox.Text) ? string.Empty : BarangayTxtbox.Text.Trim();
                string city = string.IsNullOrWhiteSpace(CityTxtbox.Text) ? string.Empty : CityTxtbox.Text.Trim();
                int zipcode = string.IsNullOrWhiteSpace(ZipCodeTxtbox.Text) ? 0 : Convert.ToInt32(ZipCodeTxtbox.Text);
                int zone = string.IsNullOrWhiteSpace(ZoneTxtbox.Text) ? 0 : Convert.ToInt32(ZoneTxtbox.Text);

                AddressModel address = new AddressModel()
                {
                    HouseNo = housenum,
                    Street = street,
                    Barangay = barangay,
                    City = city,
                    ZipCode = zipCode,
                    Zone = zone
                };

                EmergencyContactModel emergencyContact = new EmergencyContactModel()
                {
                    FirstName = firstname,
                    MiddleName = middlename,
                    LastName = lastname,
                    Suffix = suffix,
                    address = address
                };


                DialogResult YesNO = MessageBox.Show("Are you sure to submit?", "Submit information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (YesNO == DialogResult.Yes)
                {
                    try
                    {
                        Database.PatientRegFunc(emergencyContact, patient.UserName, firstname, lastname, middlename, suffix, housenum, street, barangay, city, zipcode, zone, mode);
                        MessageBox.Show("Submit Succesfully");
                        this.Hide();

                        //Refresh the patient list
                       
                        //ReloadResults.Invoke();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting data: " + ex.Message);
                    }
                    finally
                    {
                        this.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }




        }

        private void FirstnameTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(FirstnameTxtbox, FirstNameLabel, "Firstname");
        }

        private void LastNameTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(LastNameTxtbox, LastNameLabel, "Lastname");
        }

        private void MiddleNameTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(MiddleNameTxtbox, MiddleNameLabel, "Middlename");
        }

        private void SuffixTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(SuffixTxtbox, SuffixLabel, "Suffix");
        }

        private void HouseNoTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(HouseNoTxtbox, HouseLabel, "House No.");
        }

        private void ZipCodeTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ZipCodeTxtbox, ZipCodeLabel, "Zip code");
        }

        private void ZoneTxtbox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ZoneTxtbox, ZoneLabel, "Zone");
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

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }



        private void BackButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = DialogResult.Cancel;
            if (mode == ModalMode.Add)
            {
                confirm = MessageBox.Show("Are you sure you want to go back? Your progress will be lost.", "Back to Page 2", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            

            if (confirm == DialogResult.Yes || mode == ModalMode.Edit)
            {
                try
                {
                   if (mode == ModalMode.Add)
                    {
                        Database.NullPatientReg2Data(patient.UserName);
                    }
                    

                    PatientBasicInformationForm patientInfoForm = new PatientBasicInformationForm(patient, mode, panelMode);
                    patientInfoForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ExitButton_Click_1(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show($"Are you sure to cancel {(mode == ModalMode.Add ? "cancel registration" : "close")}?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (mode == ModalMode.Add)
                    {
                        Database.DeletePatientByUsername(patient.UserName);

                        MessageBox.Show("Your data has been deleted.");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting data: " + ex.Message);
                }
                finally
                {
                    this.Close();
                }
            }
        }

        private void FillUpFields()
        {
            FirstnameTxtbox.Text = patient.EmergencyContact.FirstName;
            LastNameTxtbox.Text = patient.EmergencyContact.LastName;
            MiddleNameTxtbox.Text = patient.EmergencyContact.MiddleName;
            SuffixTxtbox.Text = patient.EmergencyContact.Suffix;

            //Address
            HouseNoTxtbox.Text = patient.EmergencyContact.address.HouseNo.ToString();
            ZipCodeTxtbox.Text = patient.EmergencyContact.address.ZipCode.ToString();
            ZoneTxtbox.Text = patient.EmergencyContact.address.Zone.ToString();
            BarangayTxtbox.Text = patient.EmergencyContact.address.Barangay;
            CityTxtbox.Text = patient.EmergencyContact.address.City;
        }

        private void EmergencyRegistration_Load_1(object sender, EventArgs e)
        {
            if (mode == ModalMode.Edit)
            {
                FillUpFields();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult isDelete = MessageBox.Show("Are you sure you want to delete this information?", "Patient Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (isDelete == DialogResult.No) return;
            DeletePatientAndReload.Invoke();
            this.Hide();
        }
    }
}

