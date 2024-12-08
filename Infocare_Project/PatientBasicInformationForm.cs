using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Infocare_Project
{
    public partial class PatientBasicInformationForm : Form
    {
        private string LoggedInUsername;
        private string FirstName;
        private string LastName;

        public PatientBasicInformationForm(string usrnm, string firstName, string lastName)
        {
            InitializeComponent();
            LoggedInUsername = usrnm;
            FirstName = firstName;
            LastName = lastName;
            NameLabel.Text = $"{lastName}, {firstName}";

            HeightTextBox.TextChanged += HeightOrWeightTextChanged;
            WeightTextBox.TextChanged += HeightOrWeightTextChanged;
        }

        private void PatientBasicInformationForm_Load(object sender, EventArgs e)
        {
            LoadPatientName();
        }

        private void LoadPatientName()
        {
            Database db = new Database();
            string fullName = db.GetPatientName(LoggedInUsername);

            if (!string.IsNullOrEmpty(fullName))
            {
                NameLabel.Text = fullName;
            }
            else
            {
                NameLabel.Text = "No data found.";
            }
        }

        private void HeightOrWeightTextChanged(object sender, EventArgs e)
        {
            try
            {
                double heightCm = string.IsNullOrWhiteSpace(HeightTextBox.Text) ? 0 : Convert.ToDouble(HeightTextBox.Text);
                double weight = string.IsNullOrWhiteSpace(WeightTextBox.Text) ? 0 : Convert.ToDouble(WeightTextBox.Text);

                if (heightCm > 0 && weight > 0)
                {
                    double heightInMeters = heightCm;
                    double bmi = weight / (heightCm * heightCm); 
                    BmiTextBox.Text = bmi.ToString("F2"); 
                }
                else
                {
                    BmiTextBox.Clear(); 
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error calculating BMI: " + ex.Message);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure to cancel registration?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    Database db = new Database();
                    db.DeletePatientByUsername(LoggedInUsername);
                    MessageBox.Show("Your data has been deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting data: " + ex.Message);
                }
                finally
                {
                    this.Close();
                }
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            Database db = new Database();

            if (!db.IsValidTextInput(AlergyTextbox.Text))
            {
                MessageBox.Show("Alergy field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!db.IsValidTextInput(MedicationTxtbox.Text))
            {
                MessageBox.Show("Medication field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!db.IsValidTextInput(PreviousSurgeryTextBox.Text))
            {
                MessageBox.Show("Previous Surgery field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!db.IsValidTextInput(preConditionTextBox.Text))
            {
                MessageBox.Show("Pre-condition field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!db.IsValidTextInput(TreatmentTextBox.Text))
            {
                MessageBox.Show("Treatment field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            try
            {
                double height = string.IsNullOrWhiteSpace(HeightTextBox.Text) ? 0 : Convert.ToDouble(HeightTextBox.Text);
                double weight = string.IsNullOrWhiteSpace(WeightTextBox.Text) ? 0 : Convert.ToDouble(WeightTextBox.Text);
                double bmi = string.IsNullOrWhiteSpace(BmiTextBox.Text) ? 0 : Convert.ToDouble(BmiTextBox.Text);
                string bloodType = BloodTypeComboBox.SelectedItem?.ToString() ?? string.Empty;
                string preCon = string.IsNullOrWhiteSpace(preConditionTextBox.Text) ? string.Empty : preConditionTextBox.Text;
                string treatment = string.IsNullOrWhiteSpace(TreatmentTextBox.Text) ? string.Empty : TreatmentTextBox.Text;
                string prevSurg = string.IsNullOrWhiteSpace(PreviousSurgeryTextBox.Text) ? string.Empty : PreviousSurgeryTextBox.Text;
                string alergy = string.IsNullOrWhiteSpace(AlergyTextbox.Text) ? string.Empty : AlergyTextbox.Text;
                string medication = string.IsNullOrWhiteSpace(MedicationTxtbox.Text) ? string.Empty : MedicationTxtbox.Text;

                Patient patient = new Patient()
                {
                    Height = height,
                    Weight = weight,
                    BMI = bmi,
                    BloodType = bloodType,
                    PreCon = preCon,
                    Treatment = treatment,
                    PrevSurg = prevSurg,
                    Alergy = alergy,
                    Medication = medication
                };

                Database d1b = new Database();
                d1b.PatientReg2(patient, LoggedInUsername, height, weight, bmi, bloodType, preCon, treatment, prevSurg, alergy, medication);

                var emergencyRegistration = new EmergencyRegistration(LoggedInUsername, FirstName, LastName);
                emergencyRegistration.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


            //VALIDATION

            

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PatientRegisterForm patientRegisterForm = new PatientRegisterForm();
            patientRegisterForm.Show();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back? Your progress will be lost.", "Back to Page 1", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    Database db = new Database();
                    db.DeletePatientReg1Data(LoggedInUsername);

                    var patientInfoForm = new PatientRegisterForm();
                    patientInfoForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
