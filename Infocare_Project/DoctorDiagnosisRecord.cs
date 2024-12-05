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
    public partial class DoctorDiagnosisRecord : Form
    {
        public DoctorDiagnosisRecord()
        {
            InitializeComponent();
        }

        private void doctor_ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close? Unsaved changes will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }

        }

        private void doctor_MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            //DialogResult confirm = MessageBox.Show("Are you sure you want to go back? Your progress will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //if (confirm == DialogResult.Yes)

            //{
            //    DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();
            //    this.Hide();

            //    doctorMedicalRecord.Show();
            //    doctorMedicalRecord.BringToFront();
            //}
        }
    }
}
