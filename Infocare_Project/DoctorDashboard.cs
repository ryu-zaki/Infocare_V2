using Guna.UI2.WinForms;
using Infocare_Project;
using Infocare_Project.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class DoctorDashboard : Form
    {
        private string LoggedInUsername;
        private string FirstName;
        private string LastName;
        public DoctorDashboard(string usrnm, string firstName, string lastName)
        {
            InitializeComponent();

            LoggedInUsername = usrnm;
            FirstName = firstName;
            LastName = lastName;

            NameLabel.Text = $"Dr. {lastName}, {firstName}";
        }


        private void DoctorDashboard_Load(object sender, EventArgs e)
        {
            LoadPendingAppointments();
            LoadCompletedAppointments();
            LoadRejectedAppointments();
        }



        private void ApprovalPendingButton_Click(object sender, EventArgs e)
        {
            ReconsiderButton.Visible = false;
            CreateDiagnosisButton.Visible = false;
            AcceptButton.Visible = true;
            DeclineButton.Visible = true;
            ViewButton.Visible = false;
            CheckOutButton.Visible = false;
            InvoiceButton.Visible = false;

            Database db = new Database();

            string doctorFullName = $"Dr. {LastName}, {FirstName}";

            DataTable pendingAppointments = db.PendingAppointmentList(doctorFullName);
            DataGridViewList.DataSource = pendingAppointments;


            try
            {
                if (pendingAppointments != null && pendingAppointments.Rows.Count > 0)
                {
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
            ReconsiderButton.Visible = false;
            AcceptButton.Visible = false;
            DeclineButton.Visible = false;
            CreateDiagnosisButton.Visible = true;
            ViewButton.Visible = false;
            CheckOutButton.Visible = false;
            InvoiceButton.Visible = false;

            Database db = new Database();

            string doctorFullName = $"Dr. {LastName}, {FirstName}";
            DataTable viewappoointment = db.ViewAppointments(doctorFullName);
            DataGridViewList.DataSource = viewappoointment;

            try
            {


                if (viewappoointment != null && viewappoointment.Rows.Count > 0)
                {
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

            }
        }


        private void CreateDiagnosisButton_Click(object sender, EventArgs e)
        {
            if (DataGridViewList.SelectedRows.Count > 0)
            {
                int appointmentId = Convert.ToInt32(DataGridViewList.SelectedRows[0].Cells["id"].Value);
                Database db = new Database();

                db.CreateDiagnosis(
                    appointmentId,
                    patientDetails =>
                    {
                        string doctorFullName = $"Dr. {LastName}, {FirstName}";

                        DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();

                        doctorMedicalRecord.SetDoctorName(doctorFullName);

                        doctorMedicalRecord.SetPatientDetails(
                            patientDetails["P_Firstname"],
                            patientDetails["P_Lastname"],
                            patientDetails["P_Bdate"],
                            patientDetails["P_Height"],
                            patientDetails["P_Weight"],
                            patientDetails["P_BMI"],
                            patientDetails["P_Blood_Type"],
                            patientDetails["P_Alergy"],
                            patientDetails["P_Medication"],
                            patientDetails["P_PrevSurgery"],
                            patientDetails["P_Precondition"],
                            patientDetails["P_Treatment"]
                        );

                        doctorMedicalRecord.Show();
                    },
                    errorMessage => MessageBox.Show(errorMessage)
                );
            }
            else
            {
                MessageBox.Show("Please select an appointment.");
            }

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

                string doctorName = NameLabel.Text.Replace("!", "").Trim();
                DataTable pendingAppointments = db.PendingAppointmentList(doctorName);
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

                string doctorName = NameLabel.Text.Replace("!", "").Trim();
                DataTable pendingAppointments = db.PendingAppointmentList(doctorName);
                DataGridViewList.DataSource = pendingAppointments;

                MessageBox.Show("Selected appointments have been declined.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while declining the appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RejectedRequestsButton_Click(object sender, EventArgs e)
        {
            ReconsiderButton.Visible = true;
            AcceptButton.Visible = false;
            DeclineButton.Visible = false;
            CreateDiagnosisButton.Visible = false;
            ViewButton.Visible = false;

            Database db = new Database();
            string doctorFullName = $"Dr. {LastName}, {FirstName}";
            DataTable declinedappointment = db.DeclinedAppointments(doctorFullName);
            DataGridViewList.DataSource = declinedappointment;

            try
            {


                if (declinedappointment != null && declinedappointment.Rows.Count > 0)
                {
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

        private void DoctorDashboard_Load_1(object sender, EventArgs e)
        {

        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {

        }

        private void CompletedAppointmentsButton_Click(object sender, EventArgs e)
        {
            ReconsiderButton.Visible = false;
            AcceptButton.Visible = false;
            DeclineButton.Visible = false;
            CreateDiagnosisButton.Visible = false;
            ViewButton.Visible = true;
            CheckOutButton.Visible = true;
            InvoiceButton.Visible = true;

            Database db = new Database();

            string doctorFullName = $"Dr. {LastName}, {FirstName}";
            DataTable viewcompletedappoointment = db.ViewCompletedAppointments(doctorFullName);
            DataGridViewList.DataSource = viewcompletedappoointment;

            try
            {


                if (viewcompletedappoointment != null && viewcompletedappoointment.Rows.Count > 0)
                {
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

        private void ViewButton_Click(object sender, EventArgs e)
        {
            if (DataGridViewList.SelectedRows.Count > 0)
            {
                int appointmentId = Convert.ToInt32(DataGridViewList.SelectedRows[0].Cells["id"].Value);
                Database db = new Database();

                db.viewDocument(
                    appointmentId,
                    patientDetails =>
                    {
                        ViewPatientInformation2 viewpatientinfo = new ViewPatientInformation2();

                        viewpatientinfo.SetDetails(
                            patientDetails["P_Firstname"],
                            patientDetails["P_Lastname"],
                            patientDetails["P_Bdate"],
                            patientDetails["P_Height"],
                            patientDetails["P_Weight"],
                            patientDetails["P_BMI"],
                            patientDetails["P_Blood_Type"],
                            patientDetails["P_Alergy"],
                            patientDetails["P_Medication"],
                            patientDetails["P_PrevSurgery"],
                            patientDetails["P_Precondition"],
                            patientDetails["P_Treatment"],
                            patientDetails["ah_DoctorFirstName"],
                            patientDetails["ah_DoctorLastName"],
                            patientDetails["ah_Time"],
                            patientDetails["ah_Date"],
                            patientDetails["ah_Consfee"],
                            patientDetails["d_diagnosis"],
                            patientDetails["d_additionalnotes"],
                            patientDetails["d_doctoroder"],
                            patientDetails["d_prescription"]
                        );

                        viewpatientinfo.Show();
                    },
                    errorMessage => MessageBox.Show(errorMessage)
                );
            }
            else
            {
                MessageBox.Show("Please select an appointment.");
            }
        }

        private void CheckOutButton_Click(object sender, EventArgs e)
        {
            Database db = new Database();

            DialogResult result = MessageBox.Show("Are you sure you want to check out this appointment?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in DataGridViewList.Rows)
                    {
                        if (row.Cells["checkboxcolumn"] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                        {
                            int appointmentId = Convert.ToInt32(row.Cells["id"].Value);
                            db.CheckOutAppointment(appointmentId);
                        }
                    }

                    string doctorName = NameLabel.Text.Replace("!", "").Trim();

                    string specialization = db.GetDoctorSpecialization(doctorName);

                    DataTable checkoutAppointments = db.CheckOutAppointmentList(doctorName);

                    DataGridViewList.DataSource = checkoutAppointments;

                    DoctorBillingInvoice invoiceForm = new DoctorBillingInvoice();

                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    invoiceForm.SetDoctorDetails(doctorName, specialization, date, checkoutAppointments);

                    invoiceForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while checking out the appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private DoctorBillingInvoice _doctorBillingInvoice;

        private void InvoiceButton_Click(object sender, EventArgs e)
        {
            if (_doctorBillingInvoice == null || _doctorBillingInvoice.IsDisposed)
            {
                string doctorFullName = $"Dr. {LastName}, {FirstName}";

                Database db = new Database();
                string specialization = db.GetDoctorSpecialization(doctorFullName);

                DataTable checkoutAppointments = db.CheckOutAppointmentList(doctorFullName);

                _doctorBillingInvoice = new DoctorBillingInvoice();

                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                _doctorBillingInvoice.SetDoctorDetails(doctorFullName, specialization, currentDate, checkoutAppointments);

                _doctorBillingInvoice.Show();
            }
            else
            {
                // The form is already open, bring it to the front
                _doctorBillingInvoice.BringToFront();
            }
        }


        private void LoadPendingAppointments()
        {
            Database db = new Database();

            string doctorFullName = $"Dr. {LastName}, {FirstName}";
            DataTable pendingAppointments = db.PendingAppointmentList(doctorFullName);
            DataGridViewList.DataSource = pendingAppointments;

            try
            {
                if (pendingAppointments != null && pendingAppointments.Rows.Count > 0)
                {
                    DataGridViewList.AutoGenerateColumns = true;
                    DataGridViewList.AllowUserToAddRows = false;
                    DataGridViewList.Visible = true;
                }
                else
                {
                    MessageBox.Show("No pending appointments found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading pending appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCompletedAppointments()
        {
            Database db = new Database();

            string doctorFullName = $"Dr. {LastName}, {FirstName}";
            DataTable completedAppointments = db.ViewCompletedAppointments(doctorFullName);
            DataGridViewList.DataSource = completedAppointments;

            try
            {
                if (completedAppointments != null && completedAppointments.Rows.Count > 0)
                {
                    DataGridViewList.AutoGenerateColumns = true;
                    DataGridViewList.AllowUserToAddRows = false;
                    DataGridViewList.Visible = true;
                }
                else
                {
                    MessageBox.Show("No completed appointments found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading completed appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRejectedAppointments()
        {
            Database db = new Database();

            string doctorFullName = $"Dr. {LastName}, {FirstName}";
            DataTable rejectedAppointments = db.DeclinedAppointments(doctorFullName);
            DataGridViewList.DataSource = rejectedAppointments;

            try
            {
                if (rejectedAppointments != null && rejectedAppointments.Rows.Count > 0)
                {
                    DataGridViewList.AutoGenerateColumns = true;
                    DataGridViewList.AllowUserToAddRows = false;
                    DataGridViewList.Visible = true;
                }
                else
                {
                    MessageBox.Show("No rejected appointments found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading rejected appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pd_HomeButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Log Out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                DoctorLogin DoctorLoginForm = new DoctorLogin();
                DoctorLoginForm.Show();
                this.Hide();
            }
        }

        private void ReconsiderButton_Click(object sender, EventArgs e)
        {
            Database db = new Database();

            try
            {
                // Reconsider the selected appointments
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    if (row.Cells["checkboxcolumn"] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        int appointmentId = Convert.ToInt32(row.Cells["id"].Value);

                        db.ReconsiderAppointment(appointmentId); // Update status to 'Pending' for declined appointments
                    }
                }

                // Reload the data after reconsidering appointments
                string doctorName = NameLabel.Text.Replace("!", "").Trim();
                DataTable pendingAppointments = db.PendingAppointmentList(doctorName); // Ensure this retrieves updated data
                DataGridViewList.DataSource = pendingAppointments;

                MessageBox.Show("Selected appointments have been reconsidered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reconsidering the appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Staff_ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close? Unsaved changes will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void Staff_MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
