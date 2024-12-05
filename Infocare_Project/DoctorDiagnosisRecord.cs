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
    public partial class DoctorDiagnosisRecord : Form
    {
        public DoctorDiagnosisRecord()
        {
            InitializeComponent();
        }

        private void doctor_ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close? Unsaved changes will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }

        }

        private void doctor_MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        public void SetPatientName(string firstName, string lastName)
        {
            PatientNameLabel.Text = $"{firstName} {lastName}";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            //DialogResult confirm = MessageBox.Show("Are you sure you want to go back? Your progress will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //if (confirm == DialogResult.Yes)

            //{
            //    DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();
            //    this.Hide();

            //    doctorMedicalRecord.Show();
            //    doctorMedicalRecord.BringToFront();
            //}
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get patient name
                string[] patientName = PatientNameLabel.Text.Split(' ');
                string firstName = patientName[0];
                string lastName = patientName[1];

                // Get details from textboxes
                string diagnosis = DiagnosisTextBox.Text.Trim();
                string additionalNote = AdditionalNoteTextBox.Text.Trim();
                string doctorOrder = DoctorOrderTextBox.Text.Trim();
                string prescription = PrescriptionTextBox.Text.Trim();

                // Validate fields
                if (string.IsNullOrEmpty(diagnosis))
                {
                    MessageBox.Show("Diagnosis is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update the existing record in the database
                string query = "UPDATE tb_completedappointment " +
                               "SET d_diagnosis = @Diagnosis, d_addtionalnotes = @AdditionalNote,  d_doctororder = @DoctorOrder, d_description = @Prescription " +
                               "WHERE P_FirstName = @FirstName AND P_LastName = @LastName";
                Database db = new Database();
                db.ExecuteQuery(query, new Dictionary<string, object>
            {
                { "@Diagnosis", diagnosis },
                { "@AdditionalNote", additionalNote },
                { "@DoctorOrder", doctorOrder },
                { "@Prescription", prescription },
                { "@FirstName", firstName },
                { "@LastName", lastName }
            });

                MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optionally close the form
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
