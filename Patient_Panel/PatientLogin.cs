using Infocare_Project_1;
using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models;
using Patient_Panel;
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
    public partial class PatientLogin : Form
    {
        public PatientLogin()
        {
            InitializeComponent();
            forgotPassBtn.Cursor = Cursors.Hand;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {

            string username = UsernameTxtbox.Text;
            string password = PasswordTxtbox.Text;

            LoginEmpty loginEmpty = new LoginEmpty();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {

                MessageBox.Show("Credentials are empty", "Empty FIelds", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }


            bool validStaff = Database.RoleLogin(username, password, Role.Patient);


            if (validStaff)
            {
                PatientModel patient = Database.GetPatientInfo(username, ProcessMethods.HashCharacter(password));

                MessageBox.Show("Log in Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PatientDashboard patientdashboard = new PatientDashboard(patient);
                patientdashboard.Show();
                this.Hide();
            }

            else
            {

                MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }


        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
            this.Hide();
        }

        private void UsernameTxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void staff_HomeButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                Form1 homeForm = new Form1();
                homeForm.Show();
                this.Hide();
            }
        }

        private void staff_showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (staff_showpass.Checked)
            {
                PasswordTxtbox.PasswordChar = '\0';
                PasswordTxtbox.UseSystemPasswordChar = false;

            }
            else
            {
                PasswordTxtbox.PasswordChar = '●';
                PasswordTxtbox.UseSystemPasswordChar = true;
            }
        }

        private void forgotPassBtn_Click_1(object sender, EventArgs e)
        {
            ProcessMethods.viewForgotPass(Role.Patient);
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Register a Patient?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                PatientRegisterForm patientRegisterForm = new PatientRegisterForm(ModalMode.Add);
                patientRegisterForm.Show();
            }
        }
    }
}
