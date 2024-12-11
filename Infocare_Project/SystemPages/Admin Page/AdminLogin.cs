using Infocare_Project_1;
using Infocare_Project_1.Classes;
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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
            forgotPassBtn.Cursor = Cursors.Hand;
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {

            string username = UsernameTxtbox.Text;
            string password = PasswordTxtbox.Text;


            bool valid = Database.RoleLogin(username, password, Role.Admin);

            if (valid)
            {


                MessageBox.Show("Log in Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AdminDashboard2 adminDashboard = new AdminDashboard2();
                adminDashboard.Show();
                this.Hide();

            }
            else
            {

                MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }




            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {

                LoginEmpty nullLogin = new LoginEmpty(username, password);

                MessageBox.Show("Username or Password can't be missing", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Hide();
                return;

            }

        }


        private void admin_showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (admin_showpass.Checked)
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

        private void UsernameTxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ad_HomeButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                HomeForm homeForm = new HomeForm();
                homeForm.Show();
                this.Hide();
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

        private void forgotPassBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
