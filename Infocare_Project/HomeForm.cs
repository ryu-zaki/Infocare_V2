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
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            PatientLoginForm loginForm = new PatientLoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            LandForm landForm = new LandForm();
            landForm.Show();
            this.Close();
        }
    }
}
