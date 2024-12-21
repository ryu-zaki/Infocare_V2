﻿using Guna.UI2.WinForms;
using Infocare_Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project_1.Object_Models
{
    public partial class ResetPassword : Form
    {
        public UserModel user;
        public Action<string> SavePass;
        public Guna2TextBox[] textBoxes;
        Form emailInput;
        Form otpModal;

        public ResetPassword(Form emailInput, Form otpModal)
        {
            InitializeComponent();
            this.emailInput = emailInput;
            this.otpModal = otpModal;

            textBoxes = new Guna2TextBox[2] { newpassTextbox, confirmpassTextbox };

            newpassTextbox.TextChanged += textBox_textChanged;
            confirmpassTextbox.TextChanged += textBox_textChanged;

        }

        private void textBox_textChanged(object sender, EventArgs e)
        {
            Guna2TextBox? textBox = sender as Guna2TextBox;

            textBox.BorderColor = SystemColors.ControlDarkDark;
            textBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
        }

        private void savePassBtn_Click(object sender, EventArgs e)
        {
            if (!ProcessMethods.ValidateFields(textBoxes))
            {
                MessageBox.Show("These Fields Cannot be empty", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newpassTextbox.Text.Trim() != confirmpassTextbox.Text.Trim())
            {
                MessageBox.Show("Passwords Didn't Match", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ProcessMethods.ValidatePassword(newpassTextbox.Text))
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            SavePass.Invoke(newpassTextbox.Text);

            this.Close();
            emailInput.Close();
            otpModal.Close();

        }

        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is Guna2CheckBox checkBox)
            {
                if (checkBox.Checked)
                {
                    newpassTextbox.PasswordChar = '\0';
                    newpassTextbox.UseSystemPasswordChar = false;

                    confirmpassTextbox.PasswordChar = '\0';
                    confirmpassTextbox.UseSystemPasswordChar = false;

                }
                else
                {
                    newpassTextbox.PasswordChar = '●';
                    newpassTextbox.UseSystemPasswordChar = true;

                    confirmpassTextbox.PasswordChar = '●';
                    confirmpassTextbox.UseSystemPasswordChar = true;
                }
            }
        }

        private void newpassTextbox_TextChanged(object sender, EventArgs e)
        {
            if (newpassTextbox.Text.Trim() == "")
            {
                passValidatorMsg.Visible = false;
            }
            else
            {
                passValidatorMsg.Visible = true;
                string msg =
                !Regex.IsMatch(newpassTextbox.Text, @"[A-Z]") ? "Add at least one uppercase letter" :
                !Regex.IsMatch(newpassTextbox.Text, @"[^a-zA-Z0-9\s]") ? "Add At least one special character" : !Regex.IsMatch(newpassTextbox.Text, @"[\d]") ? "Add At least one number" : !Regex.IsMatch(newpassTextbox.Text, @".{8,}") ? "Must have at least 8 characters long" : "";

                if (msg == "")
                {

                    passValidatorMsg.Text = "*Strong Enough";
                    passValidatorMsg.ForeColor = Color.Green;
                }
                else
                {
                    passValidatorMsg.Text = "*" + msg;
                    passValidatorMsg.ForeColor = Color.Red;

                }

            }
        }
    }
}
