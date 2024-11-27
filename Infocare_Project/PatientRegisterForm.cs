using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Infocare_Project
{
    public partial class PatientRegisterForm : Form
    {
        public PatientRegisterForm()
        {
            InitializeComponent();
        }

        private void PatientRegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            //
            User.u.FirstName = FirstnameTxtbox.Text;
            User.u.LastName = LastNameTxtbox.Text;
            Patient.p.MiddleName = MiddleNameTxtbox.Text;
            User.u.Suffix = SuffixTxtbox.Text;
            User.u.Bdate= SexTxtbox.Text;
            
            User.u.Username = UsernameTxtbox.Text;
            User.u.Password = PasswordTxtbox.Text;
            User.u.ConfirmPassword = ConfirmPasswordTxtbox.Text;
            


        }
    }
}
