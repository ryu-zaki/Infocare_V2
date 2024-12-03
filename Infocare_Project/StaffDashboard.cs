using Infocare_Project;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class StaffDashboard : Form
    {
        private string LoggedInUsername;
        private string FirstName;
        private string LastName;

        private readonly Dictionary<string, int> specializationFees = new Dictionary<string, int>
        {
            { "General", 500 },
            { "Pediatrics", 800 },
            { "Obstetrics and Gynecology(OB / GYN)", 1000 },
            { "Cardiology", 1500 },
            { "Orthopedics", 1200 },
            { "Radiology", 900 }
        };

        public StaffDashboard(string usrnm, string firstName, string lastName)
        {
            InitializeComponent();

            LoggedInUsername = usrnm;
            FirstName = firstName;
            LastName = lastName;

            NameLabel.Text = $"{firstName}!";
        }

        private void PatientDashboard_Load(object sender, EventArgs e)
        {
            SpclztnComboBox();
            AppointmentDatePicker.MinDate = DateTime.Today;
        }

        private void pd_BookAppointment_Click(object sender, EventArgs e)
        {
            SelectPatientPanel.Visible = true;
            BookAppPanel.Visible = true;
            SpecPanel.Visible = false;
            pd_DoctorPanel.Visible = false;
            BookingPanel.Visible = false;

            Database db = new Database();
            List<string> patientNames = db.GetPatientNames();

            PatientComboBox.Items.Clear();
            PatientComboBox.Items.Add("Select");

            foreach (var patientName in patientNames)
            {
                PatientComboBox.Items.Add(patientName);
            }

            PatientComboBox.SelectedIndex = 0;

        }

        private void pd_ViewAppointment_Click(object sender, EventArgs e)
        {
        }

        private void pd_EditInfo_Click(object sender, EventArgs e)
        {
        }

        private void pd_SpecBtn_Click(object sender, EventArgs e)
        {
            Database db = new Database();

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

                List<string> doctorNames = db.GetDoctorNames(selectedSpecialization);

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
        }

        private void pd_DocBtn_Click(object sender, EventArgs e)
        {
            if (pd_DocBox.SelectedItem == null || pd_DocBox.SelectedItem.ToString() == "Select")
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            string selectedDoctor = pd_DocBox.SelectedItem.ToString();
            Database db = new Database();

            var timeSlots = db.GetDoctorAvailableTimes(selectedDoctor);

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
                MessageBox.Show("No time slots found for this doctor.");
            }
        }


        private void pd_DocBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pd_DocBox.SelectedItem != null && pd_DocBox.SelectedItem.ToString() != "Select")
            {
                string selectedDoctor = pd_DocBox.SelectedItem.ToString();
                Database db = new Database();

                string availability = db.GetDoctorAvailability(selectedDoctor);

                if (!string.IsNullOrEmpty(availability))
                {
                    List<DayOfWeek> availableDays = ParseDayAvailability(availability);

                    ConfigureMonthCalendar(AppointmentDatePicker, availableDays);
                }
                else
                {
                    MessageBox.Show("No date availability data found for the selected doctor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void SpclztnComboBox()
        {
            pd_SpecBox.Items.Clear();

            pd_SpecBox.Items.Add("Select");

            foreach (var specialization in specializationFees.Keys)
            {
                pd_SpecBox.Items.Add(specialization);
            }

            pd_SpecBox.SelectedIndex = 0;
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
            calendar.MinDate = DateTime.Today;
            calendar.MaxSelectionCount = 1;

            calendar.DateChanged += (s, e) =>
            {
                if (!availableDays.Contains(e.Start.DayOfWeek))
                {
                    MessageBox.Show("The selected date is unavailable for the doctor.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    calendar.SetDate(DateTime.Today);
                }
            };
        }

        private void EnterPatientButton_Click(object sender, EventArgs e)
        {
            string selectedpatient = PatientComboBox.SelectedItem.ToString();

            DialogResult result = MessageBox.Show($"You selected '{selectedpatient}' as the patient. Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SelectPatientPanel.Visible = false;
                SpecPanel.Visible = true;
                BookAppPanel.Visible = true;
                pd_DoctorPanel.Visible = false;
                BookingPanel.Visible = false;
            }
            else if (result == DialogResult.No)
            {
                SelectPatientPanel.Visible = true;
                BookAppPanel.Visible = true;
            }
        }

    }
}
