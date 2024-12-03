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
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {

            string username = UsernameTxtbox.Text;
            string password = PasswordTxtbox.Text;


            Database db = new Database();

            bool valid = db.AdminLogin(username, password);

            if (valid)
            {


                MessageBox.Show("Log in Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var adminDashboard = new AdminDashboard();
                adminDashboard.Show();
                this.Hide();

            }
            else
            {

                MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);


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

        }

        private void UsernameTxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ad_HomeButton_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
            this.Hide();
        }
    }
}
