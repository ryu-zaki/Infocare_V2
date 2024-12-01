using Infocare_Project;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Infocare_Project_1
{
    public partial class PatientDashboard : Form
    {
        private string connectionString = "Server=127.0.0.1; Database=db_infocare_project;User ID=root; Password=;";
        private string LoggedInUsername;
        private string FirstName;
        private string LastName;

        private Dictionary<string, int> specializationFees = new Dictionary<string, int>
        {
            { "General", 500 },
            { "Pediatrics", 800 },
            { "Obstetrics and Gynecology(OB / GYN)", 1000 },
            { "Cardiology", 1500 },
            { "Orthopedics", 1200 },
            { "Radiology", 900 }
        };

        public PatientDashboard(string usrnm, string firstName, string lastName)
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

            SpecPanel.Visible = true;
            BookAppPanel.Visible = true;

        }
        private void pd_ViewAppointment_Click(object sender, EventArgs e)
        {

        }


        private void pd_EditInfo_Click(object sender, EventArgs e)
        {

        }

        private void pd_SpecBox_SelectedIndexChanged(object sender, EventArgs e)
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

            if (timeSlots.Count > 0)
            {
                pd_DoctorPanel.Visible = false;
                BookingPanel.Visible = true;
                BookAppPanel.Visible = true;

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

        private void pd_DocBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pd_DocBox.SelectedItem != null && pd_DocBox.SelectedItem.ToString() != "Select")
            {
                string selectedDoctor = pd_DocBox.SelectedItem.ToString();
            }
            else
            {
            }
        }
    }
}