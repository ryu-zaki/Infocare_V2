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

namespace Infocare_Project_1
{
    public partial class DoctorBillingInvoice : Form
    {
        public DoctorBillingInvoice()
        {
            InitializeComponent();
        }
        public void SetDoctorDetails(string doctorName, string specialization, string date, DataTable transactions)
        {
            billing_DoctorNameTextbox.Text = doctorName;
            billing_Specialization.Text = specialization;

            decimal totalConsultationFee = 0;

            foreach (DataRow row in transactions.Rows)
            {
                totalConsultationFee += Convert.ToDecimal(row["ah_Consfee"]);
            }

            TotalLabel.Text = $"{totalConsultationFee:C}";
        }


        private void DoctorBillingInvoice_Load(object sender, EventArgs e)
        {

            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");

            billing_DateTextBox.Text = currentDate;

            string currentTime = DateTime.Now.ToString("hh : mm tt");

            billing_TimeTextBox.Text = currentTime;

            DataTable checkoutTable = Database.ChecOutList();

            billing_DataGridView.DataSource = checkoutTable;

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    DataTable checkoutTable = (DataTable)billing_DataGridView.DataSource;

                    string doctorName = billing_DoctorNameTextbox.Text.Trim();
                    string specialization = billing_Specialization.Text.Trim();

                    if (string.IsNullOrEmpty(doctorName) || string.IsNullOrEmpty(specialization))
                    {
                        MessageBox.Show("Doctor Name or Specialization cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    foreach (DataRow row in checkoutTable.Rows)
                    {
                        string date = Convert.ToDateTime(row["ah_date"]).ToString("dd-MM-yyyy");

                        Database.UpdateStatus(doctorName);
                    }

                    MessageBox.Show("Statuses updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating statuses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
