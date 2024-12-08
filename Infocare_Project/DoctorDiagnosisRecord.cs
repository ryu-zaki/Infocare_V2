using Infocare_Project;
using MySql.Data.MySqlClient;
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
            PatientNameLabel.Text = $"{lastName}, {firstName}";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to back? Unsaved changes will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string diagnosis = DiagnosisTextBox.Text.Trim();
                string doctorOrder = DoctorOrderTextBox.Text.Trim();
                string additionalNote = AdditionalNoteTextBox.Text.Trim();
                string prescription = PrescriptionTextBox.Text.Trim();
                string patientName = PatientNameLabel.Text;

                if (string.IsNullOrEmpty(patientName))
                {
                    MessageBox.Show("Patient name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = @"UPDATE tb_AppointmentHistory SET 
                            d_diagnosis = @Diagnosis,
                            d_doctoroder = @DoctorOrder,
                            d_additionalnotes = @AdditionalNote,
                            d_prescription = @Prescription,
                            ah_Status = @Status
                         WHERE ah_Patient_Name = @PatientName and ah_status = 'Accepted'";

                Dictionary<string, object> parameters = new()
        {
            { "@Diagnosis", string.IsNullOrEmpty(diagnosis) ? DBNull.Value : diagnosis },
            { "@DoctorOrder", string.IsNullOrEmpty(doctorOrder) ? DBNull.Value : doctorOrder },
            { "@AdditionalNote", string.IsNullOrEmpty(additionalNote) ? DBNull.Value : additionalNote },
            { "@Prescription", string.IsNullOrEmpty(prescription) ? DBNull.Value : prescription },
            { "@Status", string.IsNullOrEmpty(prescription) ? DBNull.Value : "Completed" },
            { "@PatientName", patientName }
        };

                Database db = new Database();
                db.ExecuteQuery(query, parameters);

                MessageBox.Show("Appointment details saved successfully and marked as completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void DoctorDiagnosisRecord_Load(object sender, EventArgs e)
        {
            LoadAppointmentDetails();
        }

        private void LoadAppointmentDetails()
        {
            string query = @"
        SELECT ah_doctor_name, ah_specialization, ah_consfee, ah_time, ah_date
        FROM tb_appointmenthistory
        WHERE ah_Patient_Name = @PatientName";

            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Database=db_infocare_project; User ID=root; Password=;"))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientName", PatientNameLabel.Text);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string doctorName = reader["ah_doctor_name"].ToString();
                                string[] nameParts = doctorName.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (nameParts.Length == 2)
                                {
                                    DoctorLastNameLabel.Text = nameParts[0].Trim();
                                    DoctorFirstNameLabel.Text = nameParts[1].Trim();
                                }
                                else
                                {
                                    DoctorLastNameLabel.Text = "Unknown";
                                    DoctorFirstNameLabel.Text = "Unknown";
                                }

                                DoctorSpecializationLabel.Text = reader["ah_specialization"].ToString();
                                DoctorConsultationFeeLabel.Text = $"${reader["ah_consfee"].ToString()}";
                                DoctorTimeLabel.Text = reader["ah_time"].ToString();
                                DoctorDateLabel.Text = reader["ah_date"].ToString();
                            }
                            else
                            {
                                DoctorFirstNameLabel.Text = "No record found";
                                DoctorLastNameLabel.Text = "No record found";
                                DoctorSpecializationLabel.Text = "No record found";
                                DoctorConsultationFeeLabel.Text = "No record found";
                                DoctorTimeLabel.Text = "No record found";
                                DoctorDateLabel.Text = "No record found";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}    