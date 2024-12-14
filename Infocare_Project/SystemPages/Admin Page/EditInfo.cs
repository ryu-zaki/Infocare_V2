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

namespace Infocare_Project_1.SystemPages.Admin_Page
{
    public partial class EditInfo : Form
    {
        PatientModel patient;
        public EditInfo(PatientModel patient)
        {
            InitializeComponent();
            this.patient = patient;
        }

        private void EditInfo_Load(object sender, EventArgs e)
        {
            firstnameTextbox.Text = patient.FirstName;
            lastnameTextbox.Text = patient.LastName;
            middlenameTextbox.Text = patient.MiddleName;
            suffixTextbox.Text = patient.Suffix;
            usernameTextbox.Text = patient.UserName;
            contactnumberTextbox.Text = patient.ContactNumber.ToString();
            emailTextbox.Text = patient.Email;
        }

        private void saveChanges_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
