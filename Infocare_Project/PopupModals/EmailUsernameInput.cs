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

namespace Infocare_Project_1.PopupModals
{
    public partial class EmailUsernameInput : Form
    {
        UserModel user;
        public EmailUsernameInput()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            user.UserName = usernameTextbox.Text;
            user.Password = emailTextbox.Text;

            OTP_Modal otpModal = new OTP_Modal();
            otpModal.SavePass += SavePass;
            otpModal.ShowDialog();
        }

        public void SavePass()
        {

        }
    }
}
