using Guna.UI2.WinForms;
using Infocare_Project;
using Infocare_Project.Classes;
using Infocare_Project_1.Object_Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class DoctorDashboard : Form
    {
        private DoctorModel doctor;
        public DoctorDashboard(DoctorModel doctor)
        {
            InitializeComponent();

            this.doctor = doctor;

            NameLabel.Text = $"Dr. {doctor.LastName}, {doctor.FirstName}";
        }


        private void DoctorDashboard_Load(object sender, EventArgs e)
        {
            LoadPendingAppointments();
            LoadCompletedAppointments();
            LoadRejectedAppointments();
        }

        private void LoadPendingApprovals(bool haveAnError)
        {
            DataGridViewList.DataSource = null;

            ReconsiderButton.Visible = false;
            CreateDiagnosisButton.Visible = false;
            AcceptButton.Visible = true;
            DeclineButton.Visible = true;
            ViewButton.Visible = false;
            CheckOutButton.Visible = false;
            InvoiceButton.Visible = false;

            string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";

            try
            {
                DataTable pendingAppointments = Database.PendingAppointmentList(doctorFullName);

                DataGridViewList.AutoGenerateColumns = true;
                DataGridViewList.AllowUserToAddRows = false;
                DataGridViewList.Visible = true;

                if (pendingAppointments != null && pendingAppointments.Rows.Count > 0)
                {
                    DataGridViewList.DataSource = pendingAppointments;
                }
                else if (haveAnError)
                {
                    MessageBox.Show("No appointments found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ApprovalPendingButton_Click(object sender, EventArgs e)
        {
            LoadPendingApprovals(true);
        }

        public DataTable LoadAppointmentsList()
        {
            string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";
            DataTable viewappoointment = Database.ViewAppointments(doctorFullName);
            DataGridViewList.DataSource = viewappoointment;

            return viewappoointment;
        }


        private void AppointmentListButton_Click(object sender, EventArgs e)
        {
           
            DataGridViewList.DataSource = null;

            ReconsiderButton.Visible = false;
            AcceptButton.Visible = false;
            DeclineButton.Visible = false;
            CreateDiagnosisButton.Visible = true;
            ViewButton.Visible = false;
            CheckOutButton.Visible = false;
            InvoiceButton.Visible = false;

            DataTable viewappoointment = LoadAppointmentsList();

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

                Database.CreateDiagnosis(
                    appointmentId,
                    patientDetails =>
                    {
                        string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";

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

                        doctorMedicalRecord.LoadAppointmentsList += LoadAppointmentsList;

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

            try
            {
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    if (row.Cells["checkboxcolumn"] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        int appointmentId = Convert.ToInt32(row.Cells["id"].Value);

                        Database.AcceptAppointment(appointmentId);
                    }
                }

                string doctorName = NameLabel.Text.Replace("!", "").Trim();
                DataTable pendingAppointments = Database.PendingAppointmentList(doctorName);
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

            try
            {
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    if (row.Cells["checkboxcolumn"] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        int appointmentId = Convert.ToInt32(row.Cells["id"].Value);

                        Database.DeclineAppointment(appointmentId);
                    }
                }

                string doctorName = NameLabel.Text.Replace("!", "").Trim();
                DataTable pendingAppointments = Database.PendingAppointmentList(doctorName);
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
            DataGridViewList.DataSource = null;

            ReconsiderButton.Visible = true;
            AcceptButton.Visible = false;
            DeclineButton.Visible = false;
            CreateDiagnosisButton.Visible = false;
            ViewButton.Visible = false;

            string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";
            DataTable declinedappointment = Database.DeclinedAppointments(doctorFullName);
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
            DataGridViewList.DataSource = null;
            ReconsiderButton.Visible = false;
            AcceptButton.Visible = false;
            DeclineButton.Visible = false;
            CreateDiagnosisButton.Visible = false;
            ViewButton.Visible = true;
            CheckOutButton.Visible = true;
            InvoiceButton.Visible = true; ;

            string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";
            DataTable viewcompletedappoointment = Database.ViewCompletedAppointments(doctorFullName);
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

                Database.viewDocument(
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
                            Database.CheckOutAppointment(appointmentId);
                        }
                    }

                    string doctorName = NameLabel.Text.Replace("!", "").Trim();

                    string specialization = Database.GetDoctorSpecialization(doctorName);

                     

                    DoctorBillingInvoice invoiceForm = new DoctorBillingInvoice();

                    DataTable checkoutAppointments = Database.CheckOutAppointmentList(doctorName);
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    invoiceForm.SetDoctorDetails(doctorName, specialization, date, checkoutAppointments);
                    invoiceForm.Show();

                    //Reload the whole datagridviee
                    LoadCompletedAppointments(false);

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
                string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";

                string specialization = Database.GetDoctorSpecialization(doctorFullName);

                DataTable checkoutAppointments = Database.CheckOutAppointmentList(doctorFullName);

                _doctorBillingInvoice = new DoctorBillingInvoice();

                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                _doctorBillingInvoice.SetDoctorDetails(doctorFullName, specialization, currentDate, checkoutAppointments);

                _doctorBillingInvoice.Show();
            }
            else
            {
                _doctorBillingInvoice.BringToFront();
            }
        }


        private void LoadPendingAppointments()
        {

            string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";
            DataTable pendingAppointments = Database.PendingAppointmentList(doctorFullName);
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


        private void LoadCompletedAppointments(bool includeAnErrorValidation = true)
        {

            string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";
            DataTable completedAppointments = Database.ViewCompletedAppointments(doctorFullName);
            DataGridViewList.DataSource = completedAppointments;

            try
            {
                if (completedAppointments != null && completedAppointments.Rows.Count > 0)
                {
                    DataGridViewList.AutoGenerateColumns = true;
                    DataGridViewList.AllowUserToAddRows = false;
                    DataGridViewList.Visible = true;
                }
                else if (includeAnErrorValidation)
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

            string doctorFullName = $"Dr. {doctor.LastName}, {doctor.FirstName}";
            DataTable rejectedAppointments = Database.DeclinedAppointments(doctorFullName);
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

            try
            {
                // Reconsider the selected appointments
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    if (row.Cells["checkboxcolumn"] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        int appointmentId = Convert.ToInt32(row.Cells["id"].Value);

                        Database.ReconsiderAppointment(appointmentId); // Update status to 'Pending' for declined appointments
                    }
                }

                // Reload the data after reconsidering appointments
                string doctorName = NameLabel.Text.Replace("!", "").Trim();
                DataTable pendingAppointments = Database.PendingAppointmentList(doctorName); // Ensure this retrieves updated data
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

        private void DoctorDashboard_Load_2(object sender, EventArgs e)
        {
            LoadPendingApprovals(false);

        }
    }
}
