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
            SearchPanel4.Visible = false;
            SearchPanel3.Visible = false;
            SearchPanel2.Visible = true;
            SearchPanel1.Visible = false;
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
            SearchPanel3.Visible = true;
            SearchPanel4.Visible = false;
            SearchPanel2.Visible = false;
            SearchPanel1.Visible = false;
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
            SearchPanel4.Visible = false;
            SearchPanel2.Visible = false;
            SearchPanel1.Visible = true;
            SearchPanel3.Visible = false;
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
            SearchPanel3.Visible = false;
            SearchPanel4.Visible = true;
            SearchPanel2.Visible = false;
            SearchPanel1.Visible = false;
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
            try
            {
                DataTable staffData = Database.StaffList();
                if (staffData.Rows.Count > 0)
                {
                    StaffDataGridViewList2.DataSource = staffData;
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
            try
            {
                DataTable DoctorData = Database.DoctorList();
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

            try
            {
                DataTable PatientData = Database.PatientList();
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

            try
            {
                DataTable AppointmentData = Database.AppointmentList();
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
                AdminLogin patientLoginForm = new AdminLogin();
                patientLoginForm.Show();
                this.Hide();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in StaffDataGridViewList2.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["SelectCheckBox"] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = false;
                    }

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
                DoctorDataGridViewList2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void StaffDataGridViewList2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (StaffDataGridViewList2.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                StaffDataGridViewList2.Columns[e.ColumnIndex].Name == "EditButton")
            {
                DataGridViewRow selectedRow = StaffDataGridViewList2.Rows[e.RowIndex];

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    cell.ReadOnly = false;
                }

                selectedRow.Cells["EditButton"].Value = "Save";
            }
        }

        private void StaffDataGridViewList2_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = StaffDataGridViewList2.Rows[e.RowIndex];

                if (row.ReadOnly == false)
                {
                    row.Tag = "Modified";
                }
            }
        }

        private void SearchTransactionButton_Click(object sender, EventArgs e)
        {
            string transactionId = TransactionIdTextBox.Text.Trim();
            string patientName = NameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(patientName) && patientName.Any(char.IsDigit))
            {
                MessageBox.Show("Patient name cannot contain numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(transactionId) && !transactionId.All(char.IsDigit))
            {
                MessageBox.Show("Patient ID must contain only numeric values.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(transactionId) || !string.IsNullOrEmpty(patientName))
            {
                try
                {
                    DataTable dataSource = (DataTable)PatientDataGridViewList2.DataSource;

                    if (dataSource != null)
                    {
                        string filter = "";

                        if (!string.IsNullOrEmpty(transactionId))
                        {
                            filter = $"Convert([Patient ID], 'System.String') LIKE '%{transactionId}%'";
                        }

                        if (!string.IsNullOrEmpty(patientName))
                        {
                            if (!string.IsNullOrEmpty(filter))
                            {
                                filter += " OR ";
                            }

                            string[] nameParts = patientName.Split(',');

                            if (nameParts.Length == 2)
                            {
                                string lastName = nameParts[0].Trim();
                                string firstName = nameParts[1].Trim();

                                filter += $"[First Name] LIKE '%{firstName}%' OR [Last Name] LIKE '%{lastName}%'";
                            }
                            else
                            {
                                filter += $"[First Name] LIKE '%{patientName}%' OR [Last Name] LIKE '%{patientName}%'";
                            }
                        }

                        dataSource.DefaultView.RowFilter = filter;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while filtering data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter either a Patient ID or a patient name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void ResetTransactionFilterButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataSource = (DataTable)PatientDataGridViewList2.DataSource;

                if (dataSource != null)
                {
                    dataSource.DefaultView.RowFilter = string.Empty;

                    TransactionIdTextBox.Clear();
                    NameTextBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while resetting filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string transactionId = guna2TextBox2.Text.Trim();
            string patientName = guna2TextBox1.Text.Trim();

            if (!string.IsNullOrEmpty(patientName) && patientName.Any(char.IsDigit))
            {
                MessageBox.Show("Staff name cannot contain numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(transactionId) && !transactionId.All(char.IsDigit))
            {
                MessageBox.Show("Staff ID must contain only numeric values.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(transactionId) || !string.IsNullOrEmpty(patientName))
            {
                try
                {
                    DataTable dataSource = (DataTable)StaffDataGridViewList2.DataSource;

                    if (dataSource != null)
                    {
                        string filter = "";

                        if (!string.IsNullOrEmpty(transactionId))
                        {
                            filter = $"Convert([Staff ID], 'System.String') LIKE '%{transactionId}%'";
                        }

                        if (!string.IsNullOrEmpty(patientName))
                        {
                            if (!string.IsNullOrEmpty(filter))
                            {
                                filter += " OR ";
                            }

                            string[] nameParts = patientName.Split(',');

                            if (nameParts.Length == 2)
                            {
                                string lastName = nameParts[0].Trim();
                                string firstName = nameParts[1].Trim();

                                filter += $"[First Name] LIKE '%{firstName}%' OR [Last Name] LIKE '%{lastName}%'";
                            }
                            else
                            {
                                filter += $"[First Name] LIKE '%{patientName}%' OR [Last Name] LIKE '%{patientName}%'";
                            }
                        }

                        dataSource.DefaultView.RowFilter = filter;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while filtering data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter either a Staff ID or a staff name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataSource = (DataTable)StaffDataGridViewList2.DataSource;

                if (dataSource != null)
                {
                    dataSource.DefaultView.RowFilter = string.Empty;

                    guna2TextBox2.Clear();
                    guna2TextBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while resetting filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchDoctorButton_Click(object sender, EventArgs e)
        {
            string transactionId = SearchDoctorID.Text.Trim();
            string patientName = SearchDoctorName.Text.Trim();

            if (!string.IsNullOrEmpty(patientName) && patientName.Any(char.IsDigit))
            {
                MessageBox.Show("Doctor name cannot contain numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(transactionId) && !transactionId.All(char.IsDigit))
            {
                MessageBox.Show("Doctor ID must contain only numeric values.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(transactionId) || !string.IsNullOrEmpty(patientName))
            {
                try
                {
                    DataTable dataSource = (DataTable)DoctorDataGridViewList2.DataSource;

                    if (dataSource != null)
                    {
                        string filter = "";

                        if (!string.IsNullOrEmpty(transactionId))
                        {
                            filter = $"Convert([Doctor ID], 'System.String') LIKE '%{transactionId}%'";
                        }

                        if (!string.IsNullOrEmpty(patientName))
                        {
                            if (!string.IsNullOrEmpty(filter))
                            {
                                filter += " OR ";
                            }

                            string[] nameParts = patientName.Split(',');

                            if (nameParts.Length == 2)
                            {
                                string lastName = nameParts[0].Trim();
                                string firstName = nameParts[1].Trim();

                                filter += $"[First Name] LIKE '%{firstName}%' OR [Last Name] LIKE '%{lastName}%'";
                            }
                            else
                            {
                                filter += $"[First Name] LIKE '%{patientName}%' OR [Last Name] LIKE '%{patientName}%'";
                            }
                        }

                        dataSource.DefaultView.RowFilter = filter;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while filtering data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter either a Doctor ID or a Doctor name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetDoctorButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataSource = (DataTable)DoctorDataGridViewList2.DataSource;

                if (dataSource != null)
                {
                    dataSource.DefaultView.RowFilter = string.Empty;

                    SearchDoctorName.Clear();
                    SearchDoctorID.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while resetting filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppointmentSearchButton_Click(object sender, EventArgs e)
        {
            string transactionId = SearchAppointmentID.Text.Trim();
            string patientName = SearchAppointmentName.Text.Trim();

            if (!string.IsNullOrEmpty(patientName) && patientName.Any(char.IsDigit))
            {
                MessageBox.Show("Patient name cannot contain numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(transactionId) && !transactionId.All(char.IsDigit))
            {
                MessageBox.Show("Transaction ID must contain only numeric values.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(transactionId) || !string.IsNullOrEmpty(patientName))
            {
                try
                {
                    DataTable dataSource = (DataTable)AppointmentDataGridViewList2.DataSource;

                    if (dataSource != null)
                    {
                        string filter = "";

                        if (!string.IsNullOrEmpty(transactionId))
                        {
                            filter = $"Convert([Transaction ID], 'System.String') LIKE '%{transactionId}%'";
                        }

                        if (!string.IsNullOrEmpty(patientName))
                        {
                            if (!string.IsNullOrEmpty(filter))
                            {
                                filter += " OR ";
                            }

                            string[] nameParts = patientName.Split(',');

                            if (nameParts.Length == 2)
                            {
                                string lastName = nameParts[0].Trim();
                                string firstName = nameParts[1].Trim();

                                filter += $"[Patient Name] LIKE '%{lastName}%' AND [Patient Name] LIKE '%{firstName}%'";
                            }
                            else
                            {
                                filter += $"[Patient Name] LIKE '%{patientName}%'";
                            }
                        }

                        dataSource.DefaultView.RowFilter = filter;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while filtering data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter either a transaction ID or patient name  to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetAppointmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataSource = (DataTable)AppointmentDataGridViewList2.DataSource;

                if (dataSource != null)
                {
                    dataSource.DefaultView.RowFilter = string.Empty;

                    SearchAppointmentName.Clear();
                    SearchAppointmentID.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while resetting filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
    }



}

