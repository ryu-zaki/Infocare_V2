using Infocare_Project;
using Infocare_Project_1.Object_Models;
using Patient_Panel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class PatientDashboard : Form
    {
        private PatientModel patient;

        public PatientDashboard(PatientModel patient)
        {
            InitializeComponent();
            this.patient = patient;


            NameLabel.Text = $"{patient.FirstName}!";
        }

        private void PatientDashboard_Load(object sender, EventArgs e)
        {
            LoadSpecializations();
            AppointmentDatePicker.MinDate = DateTime.Today;
            AppointmentDatePicker.MaxDate = DateTime.Today.AddMonths(5);

            pd_BookAppointment_Click(this, EventArgs.Empty);

        }

        void HomeDisplay()
        {
            SelectPatientPanel.Visible = false;
            SpecPanel.Visible = true;
            BookAppPanel.Visible = true;
            pd_DoctorPanel.Visible = false;
            BookingPanel.Visible = false;


            LoadSpecializations();
        }

        private void pd_BookAppointment_Click(object sender, EventArgs e)
        {
            HomeDisplay();
        }

        private void pd_SpecBtn_Click(object sender, EventArgs e)
        {

            if (pd_SpecBox.SelectedItem == null || pd_SpecBox.SelectedItem.ToString() == "Select")
            {
                MessageBox.Show("Please select a valid doctor's specialization.");
                return;
            }

            string selectedSpecialization = pd_SpecBox.SelectedItem.ToString();

            DialogResult result = MessageBox.Show($"You selected '{selectedSpecialization}' as the specialization. Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SelectPatientPanel.Visible = false;
                SpecPanel.Visible = false;
                pd_DoctorPanel.Visible = true;
                BookAppPanel.Visible = true;

                List<string> doctorNames = Database.GetDoctorNames(selectedSpecialization);

                pd_DocBox.Items.Clear();
                pd_DocBox.Items.Add("Select");

                foreach (var doctorName in doctorNames)
                {
                    pd_DocBox.Items.Add(doctorName);
                }

                pd_DocBox.SelectedIndex = 0;
            }
            else if (result == DialogResult.No)
            {
                SpecPanel.Visible = true;
                BookAppPanel.Visible = true;
            }

            LoadConsFee();
        }


        private void pd_DocBtn_Click(object sender, EventArgs e)
        {
            if (pd_DocBox.SelectedItem == null || pd_DocBox.SelectedItem.ToString() == "Select")
            {
                return;
            }

            string selectedDoctor = pd_DocBox.SelectedItem.ToString();
            string selectedSpecialization = pd_SpecBox.SelectedItem.ToString();

            List<string> timeSlots = Database.GetDoctorAvailableTimes(selectedDoctor, selectedSpecialization);

            SelectPatientPanel.Visible = false;
            SpecPanel.Visible = false;
            pd_DoctorPanel.Visible = false;
            BookAppPanel.Visible = true;
            BookingPanel.Visible = true;

            if (timeSlots.Count > 0)
            {
                TimeCombobox.Items.Clear();
                TimeCombobox.Items.Add("Select a Time Slot");

                foreach (var timeSlot in timeSlots)
                {
                    TimeCombobox.Items.Add(timeSlot);
                }

                TimeCombobox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No time slots found for this doctor and specialization.", "No Availability", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void pd_DocBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pd_DocBox.SelectedItem != null && pd_DocBox.SelectedItem.ToString() != "Select")
            {
                string selectedDoctor = pd_DocBox.SelectedItem.ToString();

                string availability = Database.GetDoctorAvailability(selectedDoctor);

                if (!string.IsNullOrEmpty(availability))
                {
                    List<DayOfWeek> availableDays = ParseDayAvailability(availability);

                    ConfigureMonthCalendar(AppointmentDatePicker, availableDays);
                }
                else
                {
                    MessageBox.Show("No date availability data found for the selected doctor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    decimal? consultationFee = Database.GetConsultationFee(selectedDoctor);

                    if (consultationFee.HasValue)
                    {
                        ConsFeeLbl.Text = $"{consultationFee:C}";
                    }
                    else
                    {
                        ConsFeeLbl.Text = "Not Available";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the consultation fee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private List<DayOfWeek> ParseDayAvailability(string availability)
        {
            List<DayOfWeek> days = new List<DayOfWeek>();
            string[] dayNames = availability.Split(new[] { '-', ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string day in dayNames)
            {
                if (Enum.TryParse(day.Trim(), true, out DayOfWeek dayOfWeek))
                {
                    days.Add(dayOfWeek);
                }
            }

            return days;
        }

        private void ConfigureMonthCalendar(MonthCalendar calendar, List<DayOfWeek> availableDays)
        {
            DateTime today = DateTime.Today;
            DateTime minDate = today.AddDays(4);
            DateTime maxDate = today.AddMonths(5);

            calendar.MinDate = minDate;
            calendar.MaxDate = maxDate;
            calendar.MaxSelectionCount = 1;

            calendar.DateChanged += (s, e) =>
            {
                if (e.Start < minDate || e.Start > maxDate || !availableDays.Contains(e.Start.DayOfWeek))
                {
                    MessageBox.Show("The selected date is unavailable. Please select a valid day.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    calendar.SetDate(FindNearestValidDate(e.Start, availableDays, minDate, maxDate));
                }
            };
        }

        private DateTime FindNearestValidDate(DateTime selectedDate, List<DayOfWeek> availableDays, DateTime minDate, DateTime maxDate)
        {
            for (DateTime date = selectedDate.Date; date <= maxDate; date = date.AddDays(1))
            {
                if (availableDays.Contains(date.DayOfWeek))
                {
                    return date;
                }
            }

            return minDate;
        }


        private void EnterPatientButton_Click(object sender, EventArgs e)
        {
            string selectedpatient = PatientComboBox.SelectedItem.ToString();

            if (PatientComboBox.SelectedItem == null || PatientComboBox.SelectedItem.ToString() == "Select")
            {
                MessageBox.Show("Please select a valid patient.");
                return;
            }

            DialogResult result = MessageBox.Show($"You selected '{selectedpatient}' as the patient. Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                SelectPatientPanel.Visible = false;
                SpecPanel.Visible = true;
                BookAppPanel.Visible = true;
                pd_DoctorPanel.Visible = false;
                BookingPanel.Visible = false;

                LoadSpecializations();
            }
            else if (result == DialogResult.No)
            {
                SelectPatientPanel.Visible = true;
                BookAppPanel.Visible = true;
            }
        }

        private void LoadSpecializations()
        {
            try
            {

                var specializations = Database.GetSpecialization();

                pd_SpecBox.Items.Clear();
                pd_SpecBox.Items.Add("Select");

                pd_SpecBox.Items.AddRange(specializations.ToArray());

                pd_SpecBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading specializations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadConsFee()
        {
            try
            {
                if (pd_DocBox.SelectedItem == null || pd_DocBox.SelectedItem.ToString() == "Select")
                {
                    return;
                }

                string selectedDoctor = pd_DocBox.SelectedItem.ToString();

                decimal? consultationFee = Database.GetConsultationFee(selectedDoctor);

                if (consultationFee.HasValue)
                {
                    ConsFeeLbl.Text = $"{consultationFee:C}";
                }
                else
                {
                    ConsFeeLbl.Text = "Not Available";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading consultation fee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ConfirmBookBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to confirm this booking?",
                "Confirm Booking",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {

                    string selectedPatient = $"{patient.LastName}, {patient.FirstName}";
                    string selectedDoctor = pd_DocBox.SelectedItem?.ToString() ?? string.Empty;
                    string selectedTimeSlot = TimeCombobox.SelectedItem?.ToString() ?? string.Empty;
                    DateTime appointmentDate = AppointmentDatePicker.SelectionStart;
                    string specialization = pd_SpecBox.SelectedItem?.ToString() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(selectedPatient) ||
                        string.IsNullOrWhiteSpace(selectedDoctor) ||
                        string.IsNullOrWhiteSpace(selectedTimeSlot) ||
                        string.IsNullOrWhiteSpace(specialization))
                    {
                        MessageBox.Show("Please ensure all fields are filled out correctly.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (selectedTimeSlot == "Select a Time Slot")
                    {
                        MessageBox.Show("Please select a valid time slot.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (Database.IsPatientAppointmentPendingOrAccepted(selectedPatient))
                    {
                        MessageBox.Show("This patient already has a pending or accepted appointment. They cannot book another appointment until the current one is completed.", "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (Database.IsDoctorOccupied(selectedDoctor, appointmentDate))
                    {
                        MessageBox.Show("This doctor is already occupied for the selected date. Please choose another doctor or date.", "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string feeText = ConsFeeLbl.Text.Trim().Replace("$", "").Replace("€", "").Replace("£", "").Trim();
                    feeText = new string(feeText.Where(c => Char.IsDigit(c) || c == '.').ToArray());
                    decimal consultationFee = 0;
                    if (decimal.TryParse(feeText, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out consultationFee))
                    {
                    }
                    else
                    {
                        MessageBox.Show("Invalid consultation fee format. Please check the fee and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    bool appointmentSaved = Database.SaveAppointment(selectedPatient, specialization, selectedDoctor, selectedTimeSlot, appointmentDate, consultationFee);

                    if (appointmentSaved)
                    {
                        MessageBox.Show("Appointment saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HomeDisplay();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void pd_logoutlabel_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Log Out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                StaffLogin patientLoginForm = new StaffLogin();
                patientLoginForm.Show();
                this.Hide();
            }
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {

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

        private void PatientRegistrationButton_Click(object sender, EventArgs e)
        {


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SearchPanel.Visible = true;
            ViewButton.Visible = true;
            DeleteButton.Visible = true;
            AppointmentLabel.Text = "My Appointments";
            SelectPatientPanel.Visible = false;
            SpecPanel.Visible = false;
            pd_DoctorPanel.Visible = false;
            BookingPanel.Visible = false;

            BookAppPanel.Visible = false;
            ViewAppointmentPanel.Visible = true;


            string patientName = $"{patient.LastName}, {patient.FirstName}";
            DataTable viewcompletedappoointment = Database.ViewPatientAppointments(patientName);

            if (viewcompletedappoointment.Rows.Count != 0)
            {
                AppointmentDataGridViewList2.Visible = true;
                AppointmentDataGridViewList2.DataSource = viewcompletedappoointment;
                return;
            }

            MessageBox.Show("You have no Appointments.", "Appoinment Msg");
            AppointmentDataGridViewList2.Visible = false;

        }

        private void AppointmentDataGridViewList2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
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

            Console.WriteLine("After CellValueChanged:");
            foreach (DataGridViewRow row in AppointmentDataGridViewList2.Rows)
            {
                Console.WriteLine($"Row {row.Index}: Visible={row.Visible}, Checked={(row.Cells[0].Value ?? "null")}");
            }
        }


        private void AppointmentDataGridViewList2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (AppointmentDataGridViewList2.CurrentCell is DataGridViewCheckBoxCell)
            {
                AppointmentDataGridViewList2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            if (AppointmentDataGridViewList2.SelectedRows.Count > 0)
            {
                int appointmentId = Convert.ToInt32(AppointmentDataGridViewList2.SelectedRows[0].Cells["Transaction ID"].Value);

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
                MessageBox.Show("Please enter either a transaction ID or a patient name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void ResetTransactionFilterButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataSource = (DataTable)AppointmentDataGridViewList2.DataSource;

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

        private void SelectPatientPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MyProfileTabBtn_Click(object sender, EventArgs e)
        {
            PatientRegisterForm form = new PatientRegisterForm(ModalMode.Edit, patient.AccountID)
            {
                username = patient.UserName,
                password = patient.Password
            };

            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (AppointmentDataGridViewList2.SelectedRows.Count > 0)
            {
                int appointmentId = Convert.ToInt32(AppointmentDataGridViewList2.SelectedRows[0].Cells["Transaction ID"].Value);

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this appointment?",
                    "Delete Appointment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool isDeleted = Database.DeleteAppointmentByPatient(appointmentId);

                    if (isDeleted)
                    {
                        MessageBox.Show("Appointment successfully deleted.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefreshAppointmentList();
                    }
                    else
                    {
                        MessageBox.Show("You can delete Pending and Declined appointment only. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void RefreshAppointmentList()
        {
            string patientName = $"{patient.LastName}, {patient.FirstName}";
            AppointmentDataGridViewList2.DataSource = Database.ViewPatientAppointments(patientName);
        }

        void LoadInvoiceData()
        {
            string fullName = $"{patient.LastName}, {patient.FirstName}";
            DataTable data = Database.GetInvoiceList(fullName);

            InvoiceDataGridView.DataSource = data;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            SearchPanel.Visible = true;
            ViewButton.Visible = false;
            DeleteButton.Visible = false;
            SpecPanel.Visible = false;
            BookAppPanel.Visible = false;

            InvoicePanel.Visible = true;
            InvoiceBtn.Visible = true;
            InvoiceDataGridView.Visible = true;
            InvoiceHeadTitle.Visible = true;

            ViewAppointmentPanel.Visible = false;
            LoadInvoiceData();

        }

        private void AppointmentDataGridViewList2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BookingPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void InvoiceDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                bool isChecked = (bool)InvoiceDataGridView.Rows[e.RowIndex].Cells[0].Value;

                if (isChecked)
                {
                    foreach (DataGridViewRow row in InvoiceDataGridView.Rows)
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

        private void InvoiceBtn_Click(object sender, EventArgs e)
        {
            int appointmentId = Convert.ToInt32(InvoiceDataGridView.SelectedRows[0].Cells["ID"].Value);

            Appointment appointment = Database.GetAppointmentById(appointmentId);

            PatientBillingInvoice bill = new PatientBillingInvoice(appointment);
            bill.ShowDialog();


        
        }
    }
}
