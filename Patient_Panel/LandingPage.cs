using Infocare_Project;
using Infocare_Project_1;
using System.Diagnostics;

namespace Patient_Panel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Debug.WriteLine(ProcessMethods.HashCharacter("Jhonwell22@")); 
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PatientLogin patientForm = new PatientLogin();
            patientForm.Show();

            this.Hide();



        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to close?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
