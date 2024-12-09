using Infocare_Project_1;
using Infocare_Project_1.Object_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project
{
    public partial class DoctorLogin : Form
    {
        public DoctorLogin()
        {
            InitializeComponent();
        }

        private void DoctorLogin_Load(object sender, EventArgs e)
        {

        }

        private void staff_HomeButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                HomeForm homeForm = new HomeForm();
                homeForm.Show();
                this.Hide();
            }
        }

        private void doctor_MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void doctor_ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void doctor_showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (doctor_showpass.Checked)
            {
                doctor_PasswordTxtbox.PasswordChar = '\0';
                doctor_PasswordTxtbox.UseSystemPasswordChar = false;

            }
            else
            {
                doctor_PasswordTxtbox.PasswordChar = '●';
                doctor_PasswordTxtbox.UseSystemPasswordChar = true;
            }
        }

        private void doctor_EnterButton_Click(object sender, EventArgs e)
        {
            string username = doctor_UsernameTxtbox.Text;
            string password = doctor_PasswordTxtbox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {

                MessageBox.Show("Credentials are empty", "Empty FIelds", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.Hide();
                return;

            }


            bool validDoctor = Database.RoleLogin(username, password, Role.Doctor);

            if (validDoctor)
            {
                DoctorModel doctor = Database.GetDoctorNameDetails(username);

                MessageBox.Show("Log in Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DoctorDashboard dashboard = new DoctorDashboard(doctor);
                dashboard.Show();
                this.Hide();
            }

            else
            {

                MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
        }
    }
}
