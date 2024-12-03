using Infocare_Project_1;
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
    public partial class PatientLoginForm : Form
    {
        public PatientLoginForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PatientRegisterForm patientRegisterForm = new PatientRegisterForm();
            patientRegisterForm.Show();

            this.Hide();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {



            string username = UsernameTxtbox.Text;
            string password = PasswordTxtbox.Text;



            LoginEmpty loginEmpty = new LoginEmpty();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {

                MessageBox.Show("Credentials are empty", "Empty FIelds", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.Hide();
                return;

            }

            Database db = new Database();


            bool validPatient = db.PatientLogin(username, password);


            if (validPatient)
            {
                (string firstName, string lastName) = db.GetPatientNameDetails(username);

                MessageBox.Show("Login successful!");

                var patientDashboard = new PatientDashboard(username, firstName, lastName);
                patientDashboard.Show();
                this.Hide();
            }

            else
            {

                MessageBox.Show("Invalid username or password.");


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
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
            this.Hide();
        }
    }
}
