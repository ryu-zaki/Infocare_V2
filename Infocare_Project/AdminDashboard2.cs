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


            StaffDataGridViewList.Visible = true;
            DoctorDataGridViewList.Visible = false;
            PatientDataGridViewList.Visible = false;
            AppointmentDataGridViewList.Visible = false;


            ShowStaffList();
        }

        private void ad_AppointmentList_Click(object sender, EventArgs e)
        {
            ad_docpanel.Visible = true;
            ad_staffpanel.Visible = false;
            ad_patientpanel.Visible = false;
            ad_AppointmentPanel.Visible = false;



            DoctorDataGridViewList.Visible = true;
            StaffDataGridViewList.Visible = false;
            PatientDataGridViewList.Visible = false;
            AppointmentDataGridViewList.Visible = false;




            ShowDoctorList();
        }
        private void ad_patientBtn_Click(object sender, EventArgs e)
        {
            ad_patientpanel.Visible = true;
            ad_docpanel.Visible = false;
            ad_staffpanel.Visible = false;
            ad_AppointmentPanel.Visible = false;


            PatientDataGridViewList.Visible = true;
            StaffDataGridViewList.Visible = false;
            DoctorDataGridViewList.Visible = false;
            AppointmentDataGridViewList.Visible = false;



            ShowPatientList();

        }

        private void ad_appointment_Click(object sender, EventArgs e)
        {
            ad_AppointmentPanel.Visible = true;
            ad_patientpanel.Visible = false;
            ad_docpanel.Visible = false;
            ad_staffpanel.Visible = false;

            AppointmentDataGridViewList.Visible = true;
            PatientDataGridViewList.Visible = false;
            StaffDataGridViewList.Visible = false;
            DoctorDataGridViewList.Visible = false;



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
                    StaffDataGridViewList.DataSource = staffData; // Bind the data to DataGridView
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
                    DoctorDataGridViewList.DataSource = DoctorData;
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
                    PatientDataGridViewList.DataSource = PatientData;
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
                    AppointmentDataGridViewList.DataSource = AppointmentData;
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
    }
}
