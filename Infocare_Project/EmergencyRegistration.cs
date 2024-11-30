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
        private string LoggedInUsername;

        public EmergencyRegistration(string usrnm, string firstName, string lastName)
        {
            InitializeComponent();
            LoggedInUsername = usrnm;
            NameLabel.Text = $"{lastName}, {firstName}";
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

                    var PatientLogin = new PatientLoginForm();
                    PatientLogin.Show();
                    this.Hide();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
    }

