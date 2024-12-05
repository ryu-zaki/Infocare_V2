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
            DoctorDiagnosisRecord doctorDiagnosisRecord = new DoctorDiagnosisRecord();
            DialogResult confirm = MessageBox.Show("Continue to create diagnosis?.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                this.Hide();
            }
            doctorDiagnosisRecord.Show();
            this.Hide();
        }
    }
}
