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
//using iText.Kernel.Pdf;
//using iText.Layout;
//using iText.Layout.Element;
//using iText.IO.Image;
//using System.Drawing.Imaging;

namespace Infocare_Project_1
{
    public partial class ViewPatientInformation2 : Form
    {
        public ViewPatientInformation2()
        {
            InitializeComponent();
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
            e.Graphics.DrawImage(memorying, (pagearea.Width / 2) - (this.PrintablePanel.Width / 2), this.PrintablePanel.Location.Y);
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
            diagnosis_Fname.Text = doctorFirstName; // If you have a TextBox for this
            diagnosis_Lname.Text = doctorLastName; // If you have a TextBox for this
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

        //private void ExportToPDF(Panel ExportPanel, string filePath)
        //{
        //    // Step 1: Capture the panel's content as an image
        //    Bitmap panelBitmap = new Bitmap(ExportPanel.Width, ExportPanel.Height);
        //    ExportPanel.DrawToBitmap(panelBitmap, new Rectangle(0, 0, ExportPanel.Width, ExportPanel.Height));

        //    // Step 2: Save the image temporarily
        //    string tempImagePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "panelImage.png");
        //    panelBitmap.Save(tempImagePath, System.Drawing.Imaging.ImageFormat.Png);

        //    // Step 3: Create a new PDF document and add the image
        //    using (PdfWriter writer = new PdfWriter(filePath))
        //    using (PdfDocument pdfDoc = new PdfDocument(writer))
        //    using (Document document = new Document(pdfDoc))
        //    {
        //        // Specify the iText namespace for Image
        //        iText.Layout.Element.Image image = new iText.Layout.Element.Image(iText.IO.Image.ImageDataFactory.Create(tempImagePath))
        //            .SetWidth(ExportPanel.Width)
        //            .SetHeight(ExportPanel.Height);

        //        document.Add(image);
        //    }

        //    // Inform the user that the PDF was created successfully
        //    MessageBox.Show("PDF created successfully at " + filePath);
        //}


        //private void CreatePDFButton_Click(object sender, EventArgs e)
        //{
        //    // Specify the file path for the PDF
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
        //    saveFileDialog.FileName = "PatientInformation.pdf";
        //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = saveFileDialog.FileName;
        //        ExportToPDF(this.PrintablePanel, filePath);
        //    }
        //}
    }
}
