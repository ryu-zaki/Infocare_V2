using Infocare_Project.NewFolder;
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
        private string LoggedInUsername;
        private string Firstname;
        private string Lastname;
        public EmergencyRegistration(string usrnm, string firstName, string lastName)
        {
            InitializeComponent();
            _placeHolderHandler = new PlaceHolderHandler();
            LoggedInUsername = usrnm;
            NameLabel.Text = $"{lastName}, {firstName}";
            Firstname = firstName;
            Lastname = lastName;
        }



        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult YesNO = MessageBox.Show("Are you sure to cancel registration?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (YesNO == DialogResult.Yes)
            {
                try
                {
                    Database db = new Database();
                    db.DeletePatientByUsername(LoggedInUsername);

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
            Database db = new Database();
            string fullName = db.GetPatientName(LoggedInUsername);

            if (!string.IsNullOrEmpty(fullName))
            {
                NameLabel.Text = fullName;
            }
            else
            {
                NameLabel.Text = "No data found.";
            }
        }

        private void RegisterButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from form inputs and fix type assignments
                string firstname = FirstnameTxtbox.Text.Trim();
                string middlename = MiddleNameTxtbox.Text.Trim();
                string lastname = LastNameTxtbox.Text.Trim();
                string suffix = SuffixTxtbox.Text.Trim();

                // Handling the address fields correctly
                int housenum = string.IsNullOrWhiteSpace(HouseNoTxtbox.Text) ? 0 : Convert.ToInt32(HouseNoTxtbox.Text);
                string street = string.IsNullOrWhiteSpace(StreetTxtbox.Text) ? string.Empty : StreetTxtbox.Text.Trim();
                string barangay = string.IsNullOrWhiteSpace(BarangayTxtbox.Text) ? string.Empty : BarangayTxtbox.Text.Trim();
                string city = string.IsNullOrWhiteSpace(CityTxtbox.Text) ? string.Empty : CityTxtbox.Text.Trim();
                int zipcode = string.IsNullOrWhiteSpace(ZipCodeTxtbox.Text) ? 0 : Convert.ToInt32(ZipCodeTxtbox.Text);
                int zone = string.IsNullOrWhiteSpace(ZoneTxtbox.Text) ? 0 : Convert.ToInt32(ZoneTxtbox.Text);

                // Create an EmergencyContact object with the details
                EmergencyContact emergencyContact = new EmergencyContact()
                {
                    FirstName = firstname,
                    MiddleName = middlename,
                    LastName = lastname,
                    Suffix = suffix,
                    HouseNo = housenum,
                    Street = street,
                    Barangay = barangay,
                    City = city,
                    ZipCode = zipcode,
                    Zone = zone
                };


                DialogResult YesNO = MessageBox.Show("Are you sure to submit?", "Submit information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (YesNO == DialogResult.Yes)
                {
                    try
                    {
                        // Register the patient data
                        Database db = new Database();

                        // Call PatientReg3 for emergency contact registration
                        db.PatientReg3(emergencyContact, LoggedInUsername, firstname, lastname, middlename, suffix, housenum, street, barangay, city, zipcode, zone);

                        // Open emergency registration form

                        MessageBox.Show("Submit Succesfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting data: " + ex.Message);
                    }
                    finally
                    {
                        this.Close();
                    }

                    var PatientLogin = new StaffLogin();
                    PatientLogin.Show();
                    this.Hide();

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
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back? Your progress will be lost.", "Back to Page 2", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    Database db = new Database();
                    db.NullPatientReg2Data(LoggedInUsername); 

                    var patientInfoForm = new PatientBasicInformationForm(LoggedInUsername, Firstname, Lastname);
                    patientInfoForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

    }
}

