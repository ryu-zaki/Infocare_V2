using Infocare_Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class ViewPatientInformation2 : Form
    {
        public ViewPatientInformation2()
        {
            InitializeComponent();
        }

       
        public void SetDetails(
        string firstName, string lastName, string birthDate, string height, string weight, string bmi,
        string bloodType, string allergy, string medication, string prevSurgery, string precondition,
        string treatment, string doctorFirstName, string doctorLastName, string appointmentTime,
        string appointmentDate, string consultationFee, string diagnosis, string additionalNotes,
        string doctorOrder, string prescription)
        {
            // Populate form fields
            viewinfo_Fname.Text = firstName;
            viewinfo_Lname.Text = lastName;
            viewinfo_Bdate.Text = birthDate;
            viewinfo_Height.Text = height;
            viewinfo_Weight.Text = weight;
            viewinfo_Bmi.Text = bmi;
            viewifo_Btype.Text = bloodType;
            viewinfo_Allergy.Text = allergy;
            viewinfo_Medic.Text = medication;
            viewinfo_Prevsur.Text = prevSurgery;
            viewinfo_Precon.Text = precondition;
            viewinfo_Treatment.Text = treatment;
            diagnosis_Fname.Text = doctorFirstName; 
            diagnosis_Lname.Text = doctorLastName; 
            appointmenttimeTextBox.Text = appointmentTime;
            appointmentdateTextBox.Text = appointmentDate;
            consultationTextBox.Text = consultationFee;
            viewinfo_AdditionalNotes.Text = additionalNotes;
            viewinfo_diagnosis.Text = diagnosis;
            viewinfo_prescription.Text = prescription;
            viewinfo_DoctorOrder.Text = doctorOrder;
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void ViewPatientInformation2_Load(object sender, EventArgs e)
        {

        }

        private void Print(Panel PrintPanel)
        {

            PrinterSettings ps = new PrinterSettings();
            PrintPanel = PrintablePanel;
            getprintarea(PrintPanel);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.ShowDialog();
        }

        private Bitmap memorying;

        private void getprintarea(Panel PrintPanel)
        {
            memorying = new Bitmap(PrintPanel.Width, PrintPanel.Height);
            PrintPanel.DrawToBitmap(memorying, new Rectangle(0, 0, PrintPanel.Width, PrintPanel.Height));
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            float scaleWidth = (float)pagearea.Width / PrintablePanel.Width;
            float scaleHeight = (float)pagearea.Height / PrintablePanel.Height;
            float scale = Math.Min(scaleWidth, scaleHeight); 

            e.Graphics.DrawImage(memorying, 0, 0, PrintablePanel.Width * scale, PrintablePanel.Height * scale);
        }

        private void viewinfo_BackBtn_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void viewinfo_PrintBtn_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Print this form?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                Print(this.PrintablePanel);
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.Hide();
            }
        }


        private void PrintToPDF(Panel printPanel, string filePath)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF"; 

            printDocument.PrintPage += (sender, e) =>
            {
                Bitmap memorying = new Bitmap(printPanel.Width, printPanel.Height);
                printPanel.DrawToBitmap(memorying, new Rectangle(0, 0, printPanel.Width, printPanel.Height));


                e.Graphics.DrawImage(memorying, 0, 0);
            };

            printDocument.PrintController = new StandardPrintController(); 
            printDocument.Print();
        }

        private void CreatePDFButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to make PDF of form?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    PrintToPDF(this.PrintablePanel, filePath);
                }
            }
           
        }
    }
}
