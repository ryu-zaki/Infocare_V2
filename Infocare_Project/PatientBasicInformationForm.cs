using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Infocare_Project
{
    public partial class PatientBasicInformationForm : Form
    {
        private string LoggedInUsername;
        private string FirstName;
        private string LastName;

        public PatientBasicInformationForm(string usrnm, string firstName, string lastName)
        {
            InitializeComponent();
            LoggedInUsername = usrnm;
            FirstName = firstName;
            LastName = lastName;
            NameLabel.Text = $"{lastName}, {firstName}";
        }

        private void PatientBasicInformationForm_Load(object sender, EventArgs e)
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



        private void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {

                double height = string.IsNullOrWhiteSpace(HeightTextBox.Text) ? 0 : Convert.ToDouble(HeightTextBox.Text);
                double weight = string.IsNullOrWhiteSpace(WeightTextBox.Text) ? 0 : Convert.ToDouble(WeightTextBox.Text);
                double bmi = string.IsNullOrWhiteSpace(BmiTextBox.Text) ? 0 : Convert.ToDouble(BmiTextBox.Text);
                string bloodType = BloodTypeComboBox.SelectedItem?.ToString() ?? string.Empty;
                string preCon = string.IsNullOrWhiteSpace(preConditionTextBox.Text) ? string.Empty : preConditionTextBox.Text;
                string treatment = string.IsNullOrWhiteSpace(TreatmentTextBox.Text) ? string.Empty : TreatmentTextBox.Text;
                string prevSurg = string.IsNullOrWhiteSpace(PreviousSurgeryTextBox.Text) ? string.Empty : PreviousSurgeryTextBox.Text;
                string alergy = string.IsNullOrWhiteSpace(AlergyTextbox.Text) ? string.Empty : AlergyTextbox.Text;
                string medication = string.IsNullOrWhiteSpace(MedicationTxtbox.Text) ? string.Empty : MedicationTxtbox.Text;



                Patient patient = new Patient()
                {

                    Height = height,
                    Weight = weight,
                    BMI = bmi,
                    BloodType = bloodType,
                    PreCon = preCon,
                    Treatment = treatment,
                    PrevSurg = prevSurg,
                    Alergy = alergy,
                    Medication = medication
                };

                Database db = new Database();
                db.PatientReg2(patient, LoggedInUsername, height, weight, bmi, bloodType, preCon, treatment, prevSurg, alergy, medication);

                var emergencyRegistration = new EmergencyRegistration(LoggedInUsername, FirstName, LastName);
                emergencyRegistration.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PatientRegisterForm patientRegisterForm = new PatientRegisterForm();
            patientRegisterForm.Show();
        }
    }
}
