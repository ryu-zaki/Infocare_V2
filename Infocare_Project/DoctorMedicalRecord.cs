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
using System.Xml;

namespace Infocare_Project_1
{
    public partial class DoctorMedicalRecord : Form
    {
        public DoctorMedicalRecord()
        {
            InitializeComponent();
        }

        public void SetDoctorName(string doctorFullName)
        {
            DoctorFullNameLabel.Text = doctorFullName;
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
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                this.Hide();
            }
        }


        private void ContinueButton_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show(
                "Do you want to save the information?",
                "Confirm Action",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    string firstName = FirstNameTextBox.Text.Trim();
                    string lastName = LastNameTextBox.Text.Trim();
                    string bloodType = BloodTypeTextBox.Text.Trim();
                    double bmi = double.TryParse(BMITextBox.Text.Trim(), out var bmiVal) ? bmiVal : 0;
                    double weight = double.TryParse(WeightTextBox.Text.Trim(), out var weightVal) ? weightVal : 0;
                    double height = double.TryParse(HeightTextBox.Text.Trim(), out var heightVal) ? heightVal : 0;
                    string allergy = AllergyTextBox.Text.Trim();
                    string previousSurgery = PreviousSurgeryTextBox.Text.Trim();
                    string treatment = TreatmentTextBox.Text.Trim();
                    string medication = MedicationTextBox.Text.Trim();
                    string preCondition = PreConditionTextBox.Text.Trim();
                    DateTime birthday = DateTime.TryParse(BirthdayTextBox.Text.Trim(), out var bdayVal) ? bdayVal : DateTime.MinValue;

                    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                    {
                        MessageBox.Show("First Name and Last Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string patientName = $"{lastName}, {firstName}";

                    string query = @"UPDATE tb_AppointmentHistory SET 
                        P_Bdate = @Birthday,
                        P_Height = @Height,
                        P_Weight = @Weight,
                        P_BMI = @BMI,
                        P_Blood_Type = @BloodType,
                        P_Precondition = @PreCondition,
                        P_Treatment = @Treatment,
                        P_PrevSurgery = @PreviousSurgery,
                        P_Alergy = @Allergy,
                        P_Medication = @Medication,
                        ah_status = 'Accepted'
                     WHERE ah_Patient_Name = @PatientName and ah_status = 'Accepted'";

                    Dictionary<string, object> parameters = new()
            {
                { "@Birthday", birthday == DateTime.MinValue ? DBNull.Value : birthday },
                { "@Height", height > 0 ? height : DBNull.Value },
                { "@Weight", weight > 0 ? weight : DBNull.Value },
                { "@BMI", bmi > 0 ? bmi : DBNull.Value },
                { "@BloodType", string.IsNullOrEmpty(bloodType) ? DBNull.Value : bloodType },
                { "@PreCondition", string.IsNullOrEmpty(preCondition) ? DBNull.Value : preCondition },
                { "@Treatment", string.IsNullOrEmpty(treatment) ? DBNull.Value : treatment },
                { "@PreviousSurgery", string.IsNullOrEmpty(previousSurgery) ? DBNull.Value : previousSurgery },
                { "@Allergy", string.IsNullOrEmpty(allergy) ? DBNull.Value : allergy },
                { "@Medication", string.IsNullOrEmpty(medication) ? DBNull.Value : medication },
                { "@PatientName", patientName }
            };

                    Database db = new Database();
                    db.ExecuteQuery(query, parameters);

                    MessageBox.Show("Appointment history updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DoctorDiagnosisRecord diagnosisRecord = new DoctorDiagnosisRecord();
                    diagnosisRecord.SetPatientName(firstName, lastName);
                    diagnosisRecord.Show();

                    //this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
