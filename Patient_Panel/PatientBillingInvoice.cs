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
            total += (appointment.confineDays * 250);

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
    }
}
