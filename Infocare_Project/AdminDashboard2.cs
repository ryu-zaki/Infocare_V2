using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class AdminDashboard2 : Form
    {
        public AdminDashboard2()
        {
            InitializeComponent();
            ad_staffpanel.Visible = false;
            ad_docpanel.Visible = false;


        }

        private void ad_PatientList_Click(object sender, EventArgs e)
        {
            ad_staffpanel.Visible = true;
            ad_docpanel.Visible = false;
        }

        private void ad_AppointmentList_Click(object sender, EventArgs e)
        {
            ad_docpanel.Visible = true;
            ad_staffpanel.Visible = false;
        }

        private void ad_DoctorList_Click(object sender, EventArgs e)
        {

        }
    }
}
