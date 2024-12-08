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
            DialogResult confirm = MessageBox.Show("Are you sure you want to close?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
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

                MessageBox.Show("Log in Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var staffdashboard = new StaffDashboard(username, firstName, lastName);
                staffdashboard.Show();
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
                HomeForm homeForm = new HomeForm();
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
    }
}
