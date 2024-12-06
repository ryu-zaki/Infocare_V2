using Infocare_Project;
using System;
using System.Data;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class Test_Document_Printing : Form
    {
        public Test_Document_Printing()
        {
            InitializeComponent();

        }

        private void Test_Document_Printing_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        // Method to load data into the DataGridView
        private void LoadDataIntoDataGridView()
        {
            Database db = new Database();
            DataTable dataTable = db.GetAppointmentHistory();

            DataGridViewList.DataSource = dataTable;

            // Add a checkbox column to the DataGridView
            if (!DataGridViewList.Columns.Contains("Select"))
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
                {
                    HeaderText = "Select",
                    Name = "Select",
                    Width = 50
                };
                DataGridViewList.Columns.Add(checkBoxColumn);
            }
        }




        // Button click to view the selected patient's data
        private void ViewButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DataGridViewList.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                {
                    // Fetch data from the selected row
                    string firstName = row.Cells["P_firstname"].Value?.ToString() ?? string.Empty;
                    string lastName = row.Cells["P_lastname"].Value?.ToString() ?? string.Empty;
                    string birthday = row.Cells["P_bdate"].Value?.ToString() ?? string.Empty;
                    string preCondition = row.Cells["P_precondition"].Value?.ToString() ?? string.Empty;
                    string medication = row.Cells["P_medication"].Value?.ToString() ?? string.Empty;
                    string treatment = row.Cells["P_treatment"].Value?.ToString() ?? string.Empty;
                    string previousSurgery = row.Cells["P_prevsurgery"].Value?.ToString() ?? string.Empty;
                    string allergy = row.Cells["P_alergy"].Value?.ToString() ?? string.Empty;
                    string bloodType = row.Cells["P_Blood_type"].Value?.ToString() ?? string.Empty;
                    string bmi = row.Cells["P_bmi"].Value?.ToString() ?? string.Empty;
                    string weight = row.Cells["P_weight"].Value?.ToString() ?? string.Empty;
                    string height = row.Cells["P_height"].Value?.ToString() ?? string.Empty;
                    string diagnosis = row.Cells["d_diagnosis"].Value?.ToString() ?? string.Empty;
                    string additionalNotes = row.Cells["d_additionalnotes"].Value?.ToString() ?? string.Empty;
                    string doctorOrder = row.Cells["d_doctoroder"].Value?.ToString() ?? string.Empty;
                    string prescription = row.Cells["d_prescription"].Value?.ToString() ?? string.Empty;

                    // Pass the data to the ViewPatientInformation form
                    ViewPatientInformation viewForm = new ViewPatientInformation(
                        firstName, lastName, birthday, preCondition, medication,
                        treatment, previousSurgery, allergy, bloodType, bmi,
                        weight, height, diagnosis, additionalNotes, doctorOrder, prescription
                    );

                    viewForm.Show();
                    return;
                }
            }

            MessageBox.Show("Please select a single patient to view their information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }



        // Update the visibility of the ViewButton when any checkbox is checked
        private void DataGridViewList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DataGridViewList.Columns["Select"].Index)
            {
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    row.Cells["Select"].Value = false; // Deselect all rows
                }

                // Select only the clicked row
                DataGridViewList.Rows[e.RowIndex].Cells["Select"].Value = true;
                ViewButton.Visible = true; // Show the View button
            }
        }

        
    }
}
