using Guna.UI2.WinForms;
using Infocare_Project;
using Infocare_Project_1.Object_Models;
using OtpNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project_1.PopupModals
{
    public partial class EmailUsernameInput : Form
    {
        UserModel user = new UserModel();
        Role role;
        public EmailUsernameInput(Role role)
        {
            this.role = role;
            InitializeComponent();

            usernameTextbox.TextChanged += textBoxes_TextChanged;
            emailTextbox.TextChanged += textBoxes_TextChanged;

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            textBox.BorderColor = SystemColors.ControlDarkDark;
            textBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
        }

        private void textBoxes_TextChanged(object sender, EventArgs e)
        {

        }

        public void voidCloseModals()
        {
            this.Close();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            user.UserName = usernameTextbox.Text;
            user.Email = emailTextbox.Text;
            this.Cursor = Cursors.WaitCursor;
            submitBtn.Enabled = false;

            Guna2TextBox[] fields = { usernameTextbox, emailTextbox };

            if (!ProcessMethods.ValidateFields(fields))
            {
                MessageBox.Show("These fields cannot be empty", "User Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                submitBtn.Enabled = true;
                this.Cursor = Cursors.Default;
                return;
            }

            if (!Database.IsEmailExisted(role, user))
            {
                MessageBox.Show("Email or Username is not registered yet.", "User Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                submitBtn.Enabled = true;
                this.Cursor = Cursors.Default;
                return;
            }

            //Generating an OTP
            var secretKey = KeyGeneration.GenerateRandomKey(20);
            Totp totp = new Totp(secretKey, step: 600);

            string otp = ProcessMethods.GenerateOTP(totp);

            Debug.WriteLine($"Your OTP: {otp}");
           
            ProcessMethods.SendEmail(user, otp);

            submitBtn.Enabled = true;
            this.Cursor = Cursors.Default;

            OTP_Modal otpModal = new OTP_Modal(totp, this);
            otpModal.SavePass += SavePass;
            otpModal.ShowDialog();

        }

        public void SavePass(string newPass)
        {
            

            Debug.WriteLine($"Username: {user.UserName}");
            Debug.WriteLine($"Email: {user.Email}");
            Debug.WriteLine($"$New Pass: {newPass}");

            string hashPasword = ProcessMethods.HashCharacter(newPass);
            user.Password = hashPasword;
            Debug.WriteLine($"Password: {user.Password}");
            Database.UpdateUserPassword(role, user);
            MessageBox.Show("Account Password updated.");
            this.Close();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
