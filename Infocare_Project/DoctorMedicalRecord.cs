using Infocare_Project;
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
    public partial class DoctorMedicalRecord : Form
    {
        public DoctorMedicalRecord()
        {
            InitializeComponent();
        }
        public void SetPatientDetails(string firstName, string lastName, string birthday, string height, string weight,
                                      string bmi, string bloodType, string allergy, string medication,
                                      string prevSurgery, string preCondition, string treatment)
        {
            FirstNameTextBox.Text = firstName;
            LastNameTextBox.Text = lastName;
            BirthdayTextBox.Text = birthday;
            HeightTextBox.Text = height;
            WeightTextBox.Text = weight;
            BMITextBox.Text = bmi;
            BloodTypeTextBox.Text = bloodType;
            AllergyTextBox.Text = allergy;
            MedicationTextBox.Text = medication;
            PreviousSurgeryTextBox.Text = prevSurgery;
            PreConditionTextBox.Text = preCondition;
            TreatmentTextBox.Text = treatment;
        }
        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void doctor_ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void doctor_MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back? Your progress will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            // Confirm the action
            DialogResult result = MessageBox.Show(
                "Are you sure you want to save and mark the appointment as completed?",
                "Confirm Action",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Collect values from textboxes
                    string firstName = FirstNameTextBox.Text.Trim();
                    string lastName = LastNameTextBox.Text.Trim();
                    string bloodType = BloodTypeTextBox.Text.Trim();
                    string bmi = BMITextBox.Text.Trim();
                    string weight = WeightTextBox.Text.Trim();
                    string height = HeightTextBox.Text.Trim();
                    string allergy = AllergyTextBox.Text.Trim();
                    string previousSurgery = PreviousSurgeryTextBox.Text.Trim();
                    string treatment = TreatmentTextBox.Text.Trim();
                    string medication = MedicationTextBox.Text.Trim();
                    string preCondition = PreConditionTextBox.Text.Trim();
                    string birthday = BirthdayTextBox.Text.Trim();

                    // Validate required fields
                    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                    {
                        MessageBox.Show("First Name and Last Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if the patient already exists
                    Database db = new Database();
                    bool patientExists = db.IsPatientExist(firstName, lastName);
                    if (patientExists)
                    {
                        MessageBox.Show("This patient already exists in the database.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // If patient doesn't exist, insert the record
                    string query = "INSERT INTO tb_completedappointment " +
                                   "(P_FirstName, P_LastName, P_Blood_Type, P_BMI, P_Weight, P_Height, P_Alergy, P_PrevSurgery, P_Treatment, P_Medication, P_PreCondition, P_Bdate) " +
                                   "VALUES (@FirstName, @LastName, @BloodType, @BMI, @Weight, @Height, @Allergy, @PreviousSurgery, @Treatment, @Medication, @PreCondition, @Birthday)";
                    db.ExecuteQuery(query, new Dictionary<string, object>
            {
                { "@FirstName", firstName },
                { "@LastName", lastName },
                { "@BloodType", bloodType },
                { "@BMI", bmi },
                { "@Weight", weight },
                { "@Height", height },
                { "@Allergy", allergy },
                { "@PreviousSurgery", previousSurgery },
                { "@Treatment", treatment },
                { "@Medication", medication },
                { "@PreCondition", preCondition },
                { "@Birthday", birthday }
            });

                    // Open DoctorDiagnosisRecord form
                    DoctorDiagnosisRecord diagnosisRecord = new DoctorDiagnosisRecord();
                    diagnosisRecord.SetPatientName(firstName, lastName); // Pass patient data
                    diagnosisRecord.Show();

                    // Optionally close this form
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
