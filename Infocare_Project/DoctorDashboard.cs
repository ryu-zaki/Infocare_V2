using Guna.UI2.WinForms;
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

        }

        private void DataGridViewList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AppointmentListButton_Click(object sender, EventArgs e)
        {
            try
            {
                Database_DataGridView.PatientList data = new Database_DataGridView.PatientList();
                DataTable patientTable = data.GetPatientList();
                DataGridViewList.DataSource = patientTable;
                DataGridViewList.AutoGenerateColumns = false;
                DataGridViewList.AllowUserToAddRows = false;
                DataGridViewList.Visible = true;
                // Check if the first column is a checkbox column
                if (DataGridViewList.Columns[0] is DataGridViewCheckBoxColumn)
                {
                    // Optionally, set initial state for checkboxes (e.g., unchecked)
                    foreach (DataGridViewRow row in DataGridViewList.Rows)
                    {
                        if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell)
                        {
                            checkBoxCell.Value = false; // Set the checkbox to unchecked
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading patients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0) // Replace with your checkbox column index if different
            {
                e.Cancel = true; // Prevent editing for other columns
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
                AcceptButton.Visible = isAnyChecked;
                DeclineButton.Visible = isAnyChecked;
            }
        }

        private void CreateDiagnosisButton_Click(object sender, EventArgs e)
        {
            DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();
            doctorMedicalRecord.Show();
        }
    }
}
