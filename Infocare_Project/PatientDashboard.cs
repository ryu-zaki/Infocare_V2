using MySqlX.XDevAPI.Common;
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
    public partial class PatientDashboard : Form
    {
        public PatientDashboard()
        {
            InitializeComponent();
            BookAppPanel.Visible = false;
            SpecPanel.Visible = false;
            pd_DoctorPanel.Visible = false;
        }

        private void pd_BookAppointment_Click(object sender, EventArgs e)
        {

            SpecPanel.Visible = true;
            BookAppPanel.Visible = true;

        }
        private void pd_ViewAppointment_Click(object sender, EventArgs e)
        {

        }


        private void pd_EditInfo_Click(object sender, EventArgs e)
        {

        }

        private void pd_SpecBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pd_SpecBtn_Click(object sender, EventArgs e)
        {
            if (pd_SpecBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }


            if (pd_SpecBox.SelectedItem == "Balmond")
            {
                DialogResult result = MessageBox.Show("Is Balmond your selected character?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SpecPanel.Visible = false;
                    pd_DoctorPanel.Visible = true;
                    BookAppPanel.Visible = true;

                }
                else if (result == DialogResult.No)
                {
                    BookAppPanel.Visible = true;
                    SpecPanel.Visible = true;
                }
            }
        }
    }
}
