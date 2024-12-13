using Guna.UI2.WinForms;
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

            if (!ProcessMethods.ValidatePassword(newpassTextbox.Text)) return;

            this.Cursor = Cursors.WaitCursor;
            SavePass.Invoke(newpassTextbox.Text);

            this.Close();
            emailInput.Close();
            otpModal.Close();
            
        }
    }
}
