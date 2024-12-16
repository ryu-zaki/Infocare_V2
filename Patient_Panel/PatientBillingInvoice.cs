using Guna.UI2.WinForms;
using Infocare_Project_1.Object_Models;
using Org.BouncyCastle.Asn1.Mozilla;
using OtpNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace Patient_Panel
{
    public partial class PatientBillingInvoice : Form
    {
        Appointment appointment;
        decimal total = 0;
        public PatientBillingInvoice(Appointment appointment)
        {
            this.appointment = appointment;
            InitializeComponent();
        }

        static Dictionary<string, decimal> StringToDictionary(string input)
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();

            string[] pairs = input.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string pair in pairs)
            {
                string[] keyValue = pair.Split(' ');

                if (keyValue.Length == 2)
                {
                    string key = keyValue[0];
                    if (decimal.TryParse(keyValue[1], out decimal value))
                    {
                        result[key] = value;
                    }
                }

                if (keyValue.Length == 3)
                {
                    string key = keyValue[0];
                    if (decimal.TryParse(keyValue[2], out decimal value))
                    {
                        result[key] = value;
                    }
                }
            }

            return result;
        }

        public void SetupAppointmentData()
        {
            DataTable data = new DataTable();
            data.Columns.Add("Patient Name");
            data.Columns.Add("Doctor Name");
            data.Columns.Add("Time", typeof(TimeSpan));
            data.Columns.Add("Date", typeof(DateTime));
            data.Columns.Add("Consultation Fee", typeof(decimal));

            data.Rows.Add(appointment.PatientName, appointment.DoctorName, appointment.Time, appointment.Date, appointment.ConsultationFee);

            pbilling_DataGridView1.DataSource = data;
        }

        void SetupDataGridView(Guna2DataGridView gridview, Dictionary<string, decimal> valuePairs, string category)
        {
            DataTable data = new DataTable();
            data.Columns.Add(category);
            data.Columns.Add("Price", typeof(decimal));



            foreach (KeyValuePair<string, decimal> kvp in valuePairs)
            {
                total += kvp.Value;
                data.Rows.Add(kvp.Key, kvp.Value);
            }

            gridview.DataSource = data;
        }


        private void PatientBillingInvoice_Load(object sender, EventArgs e)
        {
            SetupAppointmentData();

            SetupDataGridView(pbilling_DataGridView2, StringToDictionary(appointment.Diagnosis.DoctorOrders), "Doctor Order");

            SetupDataGridView(pbilling_DataGridView3, StringToDictionary(appointment.Diagnosis.Prescription), "Prescription");

            pbilling_PatientNameTextbox.Text = appointment.PatientName;
            pbilling_DateTextbox.Text = appointment.Date.ToString("d");
            pbilling_TimeTextbox.Text = appointment.Time.ToString("hh\\:mm");
            guna2TextBox1.Text = appointment.confineDays.ToString();
            total += (appointment.confineDays * 250 + appointment.ConsultationFee);

            pbilling_TotalLabel.Text = $"₱{total}";
        }

        private void pbilling_ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbilling_MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbilling_PrintButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to Print this form?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                Print(this.PrintablePanel);
            }
        }

        private void pbilling_CreatePDFButton_Click(object sender, EventArgs e)
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            float scaleWidth = (float)pagearea.Width / PrintablePanel.Width;
            float scaleHeight = (float)pagearea.Height / PrintablePanel.Height;
            float scale = Math.Min(scaleWidth, scaleHeight);

            e.Graphics.DrawImage(memorying, 0, 0, PrintablePanel.Width * scale, PrintablePanel.Height * scale);
        }
    }
}
