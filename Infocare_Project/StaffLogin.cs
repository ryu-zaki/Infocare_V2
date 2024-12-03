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
    public partial class StaffLogin : Form
    {
        public StaffLogin()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

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


            bool validStaff = db.StaffLogin(username, password);


            if (validStaff)
            {
                (string firstName, string lastName) = db.GetStaffNameDetails(username);

                MessageBox.Show("Login successful!");

                var staffdashboard = new StaffDashboard(username, firstName, lastName);
                staffdashboard.Show();
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
