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
    public partial class PatientLoginForm : Form
    {
        public PatientLoginForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PatientRegisterForm patientRegisterForm = new PatientRegisterForm();
            patientRegisterForm.Show();

            this.Hide();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            
            string username = UsernameTxtbox.Text;
            string password = PasswordTxtbox.Text;

            
            Database db = new Database();

           
            bool validPatient = db.PatientLogin(username, password);

            if (validPatient)
            {
                
                MessageBox.Show("Login successful!");

               
            }
            else
            {
                
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}
