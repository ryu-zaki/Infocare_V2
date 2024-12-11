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
        public Action SavePass;
        public ResetPassword()
        {
            InitializeComponent();;
        }

        private void savePassBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
