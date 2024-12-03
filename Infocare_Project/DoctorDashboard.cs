using Guna.UI2.WinForms;
using Infocare_Project;
using Infocare_Project.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class DoctorDashboard : Form
    {
        public DoctorDashboard()
        {
            InitializeComponent();
        }

        private void DoctorDashboard_Load(object sender, EventArgs e)
        {

        }

        private void ApprovalPendingButton_Click(object sender, EventArgs e)
        {
            CreateDiagnosisButton.Visible = false;
            AcceptButton.Visible = true;
            DeclineButton.Visible = true;

            try
            {
                Database db = new Database();
                DataTable pendingAppointments = db.PendingAppointmentList();

                if (pendingAppointments != null && pendingAppointments.Rows.Count > 0)
                {
                    DataGridViewList.DataSource = pendingAppointments;
                    DataGridViewList.AutoGenerateColumns = true;
                    DataGridViewList.AllowUserToAddRows = false;
                    DataGridViewList.Visible = true;
                }
                else
                {
                    MessageBox.Show("No appointments found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void DataGridViewList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AppointmentListButton_Click(object sender, EventArgs e)
        {
            AcceptButton.Visible = false;
            DeclineButton.Visible = false;
            CreateDiagnosisButton.Visible = true;

            try
            {
                Database db = new Database();
                DataTable viewappoointment = db.ViewAppointments();

                if (viewappoointment != null && viewappoointment.Rows.Count > 0)
                {
                    DataGridViewList.DataSource = viewappoointment;
                    DataGridViewList.AutoGenerateColumns = true;
                    DataGridViewList.AllowUserToAddRows = false;
                    DataGridViewList.Visible = true;
                }
                else
                {
                    MessageBox.Show("No appointments found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        private void DataGridViewList_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void DataGridViewList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DataGridViewList.CurrentCell is DataGridViewCheckBoxCell)
            {
                // Commit the edit when a checkbox is clicked
                DataGridViewList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridViewList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0) // Replace with your checkbox column index if different
            {
                e.Cancel = true; // Prevent editing for other columns
            }
        }

        private void DataGridViewList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0) // Assuming the checkbox column is the first column
            {
                // Check if any checkbox is checked
                bool isAnyChecked = false;
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        isAnyChecked = true;
                        break;
                    }
                }

                // Set the visibility of the Accept and Decline buttons based on the checkbox state
            }
        }

        private void CreateDiagnosisButton_Click(object sender, EventArgs e)
        {
            DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();
            doctorMedicalRecord.Show();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            try
            {
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    if (row.Cells["checkboxcolumn"] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        int appointmentId = Convert.ToInt32(row.Cells["id"].Value);

                        db.AcceptAppointment(appointmentId);
                    }
                }

                DataTable pendingAppointments = db.PendingAppointmentList();
                DataGridViewList.DataSource = pendingAppointments;

                MessageBox.Show("Selected appointments have been accepted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while accepting the appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeclineButton_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            try
            {
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    if (row.Cells["checkboxcolumn"] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        int appointmentId = Convert.ToInt32(row.Cells["id"].Value);

                        db.DeclineAppointment(appointmentId);
                    }
                }

                DataTable pendingAppointments = db.PendingAppointmentList();
                DataGridViewList.DataSource = pendingAppointments;

                MessageBox.Show("Selected appointments have been declined.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while declining the appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void RejectedRequestsButton_Click(object sender, EventArgs e)
        {
            AcceptButton.Visible = false;
            DeclineButton.Visible = false;
            CreateDiagnosisButton.Visible = false;

            try
            {
                Database db = new Database();
                DataTable declinedappointment = db.DeclinedAppointments();

                if (declinedappointment != null && declinedappointment.Rows.Count > 0)
                {
                    DataGridViewList.DataSource = declinedappointment;
                    DataGridViewList.AutoGenerateColumns = true;
                    DataGridViewList.AllowUserToAddRows = false;
                    DataGridViewList.Visible = true;
                }
                else
                {
                    MessageBox.Show("No appointments found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
