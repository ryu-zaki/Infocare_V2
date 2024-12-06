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
    public partial class AdminDashboard2 : Form
    {
        public AdminDashboard2()
        {
            InitializeComponent();
            ad_staffpanel.Visible = false;
            ad_docpanel.Visible = false;
            ad_patientpanel.Visible = false;
            ad_AppointmentPanel.Visible = false;

        }

        private void AdminDashboard2_Load(object sender, EventArgs e)
        {
        }

        private void ad_PatientList_Click(object sender, EventArgs e)
        {
            ad_staffpanel.Visible = true;
            ad_docpanel.Visible = false;
            ad_patientpanel.Visible = false;
            ad_AppointmentPanel.Visible = false;


            StaffDataGridViewList2.Visible = true;
            DoctorDataGridViewList2.Visible = false;
            PatientDataGridViewList2.Visible = false;
            AppointmentDataGridViewList2.Visible = false;


            ShowStaffList();
        }

        private void ad_AppointmentList_Click(object sender, EventArgs e)
        {
            ad_docpanel.Visible = true;
            ad_staffpanel.Visible = false;
            ad_patientpanel.Visible = false;
            ad_AppointmentPanel.Visible = false;



            DoctorDataGridViewList2.Visible = true;
            StaffDataGridViewList2.Visible = false;
            PatientDataGridViewList2.Visible = false;
            AppointmentDataGridViewList2.Visible = false;




            ShowDoctorList();
        }
        private void ad_patientBtn_Click(object sender, EventArgs e)
        {
            ad_patientpanel.Visible = true;
            ad_docpanel.Visible = false;
            ad_staffpanel.Visible = false;
            ad_AppointmentPanel.Visible = false;


            PatientDataGridViewList2.Visible = true;
            StaffDataGridViewList2.Visible = false;
            DoctorDataGridViewList2.Visible = false;
            AppointmentDataGridViewList2.Visible = false;



            ShowPatientList();

        }

        private void ad_appointment_Click(object sender, EventArgs e)
        {
            ad_AppointmentPanel.Visible = true;
            ad_patientpanel.Visible = false;
            ad_docpanel.Visible = false;
            ad_staffpanel.Visible = false;

            AppointmentDataGridViewList2.Visible = true;
            PatientDataGridViewList2.Visible = false;
            StaffDataGridViewList2.Visible = false;
            DoctorDataGridViewList2.Visible = false;



            ShowAppointmentList();
        }


        private void AddDoctor_Click(object sender, EventArgs e)
        {
            AdminAddDoctor adminAddDoctor = new AdminAddDoctor();
            adminAddDoctor.Show();
        }

        private void ad_DoctorList_Click(object sender, EventArgs e)
        {

        }

        private void BackButton_Click(object sender, EventArgs e)
        {

        }

        private void BackButton_Click_1(object sender, EventArgs e)
        {
            AddStaff addstaff = new AddStaff();
            addstaff.Show();
        }


        private void ShowStaffList()
        {

            Database db = new Database();
            try
            {
                DataTable staffData = db.StaffList();
                if (staffData.Rows.Count > 0)
                {
                    StaffDataGridViewList2.DataSource = staffData; // Bind the data to DataGridView
                }
                else
                {
                    MessageBox.Show("No staff data found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading staff data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDoctorList()
        {

            Database db = new Database();
            try
            {
                DataTable DoctorData = db.DoctorList();
                if (DoctorData.Rows.Count > 0)
                {
                    DoctorDataGridViewList2.DataSource = DoctorData;
                }
                else
                {
                    MessageBox.Show("No Doctor data found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctor data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowPatientList()
        {

            Database db = new Database();
            try
            {
                DataTable PatientData = db.PatientList();
                if (PatientData.Rows.Count > 0)
                {
                    PatientDataGridViewList2.DataSource = PatientData;
                }
                else
                {
                    MessageBox.Show("No patient data found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctor data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAppointmentList()
        {

            Database db = new Database();
            try
            {
                DataTable AppointmentData = db.AppointmentList();
                if (AppointmentData.Rows.Count > 0)
                {
                    AppointmentDataGridViewList2.DataSource = AppointmentData;
                }
                else
                {
                    MessageBox.Show("No Appointment History data found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Appointment History data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void ad_logoutlabel_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Log Out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                AdminLogin AdminLoginForm = new AdminLogin();
                AdminLoginForm.Show();
                this.Hide();
            }
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Log Out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                AdminLogin AdminLoginForm = new AdminLogin();
                AdminLoginForm.Show();
                this.Hide();
            }
        }

        private void LogOutButton_Click_1(object sender, EventArgs e)
        {

        }

        private void AddDoctor_Click_1(object sender, EventArgs e)
        {
            AdminAddDoctor adminAddDoctor = new AdminAddDoctor();
            adminAddDoctor.Show();
        }

        private void pd_HomeButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Log Out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                StaffLogin patientLoginForm = new StaffLogin();
                patientLoginForm.Show();
                this.Hide();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in StaffDataGridViewList2.Rows)
            {
                // Check if the checkbox column is selected
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["SelectCheckBox"] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                {
                    // Make the entire row editable
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = false; // Set ReadOnly to false to allow editing
                    }

                    // Optionally, uncheck the checkbox after enabling editing
                    checkBoxCell.Value = false;
                }
            }
        }
        private void StaffDataGridViewList2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        private void StaffDataGridViewList2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                bool isChecked = (bool)StaffDataGridViewList2.Rows[e.RowIndex].Cells[0].Value;


                if (isChecked)
                {
                    foreach (DataGridViewRow row in StaffDataGridViewList2.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                            if (checkBoxCell != null)
                            {
                                checkBoxCell.Value = false;
                            }
                        }
                    }
                }
            }
        }

        private void StaffDataGridViewList2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (StaffDataGridViewList2.CurrentCell is DataGridViewCheckBoxCell)
            {
                StaffDataGridViewList2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void AppointmentDataGridViewList2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        private void AppointmentDataGridViewList2_CellBeginEdit_1(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        private void AppointmentDataGridViewList2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                bool isChecked = (bool)AppointmentDataGridViewList2.Rows[e.RowIndex].Cells[0].Value;


                if (isChecked)
                {
                    foreach (DataGridViewRow row in AppointmentDataGridViewList2.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                            if (checkBoxCell != null)
                            {
                                checkBoxCell.Value = false;
                            }
                        }
                    }
                }
            }
        }

        private void AppointmentDataGridViewList2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (AppointmentDataGridViewList2.CurrentCell is DataGridViewCheckBoxCell)
            {

                AppointmentDataGridViewList2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void PatientDataGridViewList2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        private void PatientDataGridViewList2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                bool isChecked = (bool)PatientDataGridViewList2.Rows[e.RowIndex].Cells[0].Value;

                if (isChecked)
                {
                    foreach (DataGridViewRow row in PatientDataGridViewList2.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                            if (checkBoxCell != null)
                            {
                                checkBoxCell.Value = false;
                            }
                        }
                    }
                }
            }
        }

        private void PatientDataGridViewList2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (PatientDataGridViewList2.CurrentCell is DataGridViewCheckBoxCell)
            {
                PatientDataGridViewList2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DoctorDataGridViewList2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }


        private void DoctorDataGridViewList2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                bool isChecked = (bool)DoctorDataGridViewList2.Rows[e.RowIndex].Cells[0].Value;

                if (isChecked)
                {
                    foreach (DataGridViewRow row in DoctorDataGridViewList2.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                            if (checkBoxCell != null)
                            {
                                checkBoxCell.Value = false;
                            }
                        }
                    }
                }
            }
        }

        private void DoctorDataGridViewList2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DoctorDataGridViewList2.CurrentCell is DataGridViewCheckBoxCell)
            {
                // Commit the edit when a checkbox is clicked
                DoctorDataGridViewList2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void StaffDataGridViewList2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Check if the clicked cell belongs to the "Edit" button column
            if (StaffDataGridViewList2.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                StaffDataGridViewList2.Columns[e.ColumnIndex].Name == "EditButton")
            {
                // Get the selected row
                DataGridViewRow selectedRow = StaffDataGridViewList2.Rows[e.RowIndex];

                // Make the selected row editable
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    cell.ReadOnly = false;
                }

                // Optionally, change the button text to "Save" or any other action
                selectedRow.Cells["EditButton"].Value = "Save";
            }
        }

        private void StaffDataGridViewList2_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = StaffDataGridViewList2.Rows[e.RowIndex];

                // Check if the row is no longer ReadOnly (was edited)
                if (row.ReadOnly == false)
                {
                    // Mark the row for update (e.g., by adding it to a list)
                    row.Tag = "Modified";
                }
            }
        }
    }
}
