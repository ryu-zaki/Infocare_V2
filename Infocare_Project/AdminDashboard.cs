using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infocare_Project.Classes;

namespace Infocare_Project
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void PatientListButton_Click(object sender, EventArgs e)
        {

        }

        private void DoctorListButton_Click(object sender, EventArgs e)
        {

        }

        private void AppointmentListButton_Click(object sender, EventArgs e)
        {

        }

        private void AddDoctorButton_Click(object sender, EventArgs e)
        {
             // Replace "Infocare_Project.AdminAddDoctors" with the fully qualified name of the form you want to open
            FormOpener.OpenForm("Infocare_Project.AdminAddDoctor");
          

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
