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
    public partial class OTP_Modal : Form
    {
        public Action SavePass;
        public OTP_Modal()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            ResetPassword resetPass = new ResetPassword();
            resetPass.SavePass += SavePass;
        }
    }
}
