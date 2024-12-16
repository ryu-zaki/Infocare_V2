using Infocare_Project_1;
using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Infocare_Project
{
    public partial class PatientBasicInformationForm : Form
    {
        PatientModel patient;
        ModalMode mode;
        public Action ReloadResults;
        public Action DeletePatientAndReload;
        public PatientBasicInformationForm(PatientModel patient, ModalMode mode)
        {
            InitializeComponent();
            this.mode = mode;
            this.patient = patient;
            HeightTextBox.TextChanged += HeightOrWeightTextChanged;
            WeightTextBox.TextChanged += HeightOrWeightTextChanged;

            string[] bloodtypes = { "Select BloodType", "A+", "A", "B+", "O+", "O", "O", "AB", "AB.", "AB-" };
            BloodTypeComboBox.DataSource = bloodtypes;

            if (mode == ModalMode.Edit)
            {
                DeleteBtn.Visible = true;
            }

        }

        private void LoadPatientName()
        {

            string fullName = Database.GetPatientName(patient);

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
            DialogResult confirm = MessageBox.Show($"Are you sure to cancel {(mode == ModalMode.Add ? "cancel registration" : "close")}?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (mode == ModalMode.Add)
                    {
                        Database.DeletePatientByUsername(patient.UserName);

                        MessageBox.Show("Your data has been deleted.");
                    }

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

            if (!ProcessMethods.IsValidTextInput(AlergyTextbox.Text))
            {
                MessageBox.Show("Alergy field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ProcessMethods.IsValidTextInput(MedicationTxtbox.Text))
            {
                MessageBox.Show("Medication field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ProcessMethods.IsValidTextInput(PreviousSurgeryTextBox.Text))
            {
                MessageBox.Show("Previous Surgery field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ProcessMethods.IsValidTextInput(preConditionTextBox.Text))
            {
                MessageBox.Show("Pre-condition field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ProcessMethods.IsValidTextInput(TreatmentTextBox.Text))
            {
                MessageBox.Show("Treatment field must contain only letters, spaces, or 'N/A'.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            double height = string.IsNullOrWhiteSpace(HeightTextBox.Text) ? 0 : Convert.ToDouble(HeightTextBox.Text);
            double weight = string.IsNullOrWhiteSpace(WeightTextBox.Text) ? 0 : Convert.ToDouble(WeightTextBox.Text);
            double bmi = string.IsNullOrWhiteSpace(BmiTextBox.Text) ? 0 : Convert.ToDouble(BmiTextBox.Text);
            string bloodType = BloodTypeComboBox.SelectedItem?.ToString() ?? string.Empty;
            string preCon = string.IsNullOrWhiteSpace(preConditionTextBox.Text) ? string.Empty : preConditionTextBox.Text;
            string treatment = string.IsNullOrWhiteSpace(TreatmentTextBox.Text) ? string.Empty : TreatmentTextBox.Text;
            string prevSurg = string.IsNullOrWhiteSpace(PreviousSurgeryTextBox.Text) ? string.Empty : PreviousSurgeryTextBox.Text;
            string alergy = string.IsNullOrWhiteSpace(AlergyTextbox.Text) ? string.Empty : AlergyTextbox.Text;
            string medication = string.IsNullOrWhiteSpace(MedicationTxtbox.Text) ? string.Empty : MedicationTxtbox.Text;

            HealthInfoModel healthInfo = new HealthInfoModel
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

            patient.HealthInfo = healthInfo;

            PatientModel editedInfo = SetupInfo();

            Database.PatientRegFunc(mode == ModalMode.Add ? patient : editedInfo, patient.UserName, height, weight, bmi, bloodType, preCon, treatment, prevSurg, alergy, medication, mode);


            var emergencyRegistration = new EmergencyRegistration(patient, mode);
            emergencyRegistration.ReloadResults += ReloadResults;
            emergencyRegistration.DeletePatientAndReload += DeletePatientAndReload;
            emergencyRegistration.TopMost = true;
            emergencyRegistration.Show();
            this.Hide();



            //VALIDATION



        }


        public PatientModel SetupInfo()
        {
            PatientModel editedInfo = patient;
            HealthInfoModel healthInfo = new HealthInfoModel();

            healthInfo.Weight = double.Parse(WeightTextBox.Text);
            healthInfo.Height = double.Parse(HeightTextBox.Text);
            healthInfo.BMI = double.Parse(BmiTextBox.Text);
            healthInfo.BloodType = BloodTypeComboBox.SelectedItem.ToString();
            healthInfo.Alergy = AlergyTextbox.Text;
            healthInfo.Medication = MedicationTxtbox.Text;
            healthInfo.PrevSurg = PreviousSurgeryTextBox.Text;
            healthInfo.PreCon = preConditionTextBox.Text;
            healthInfo.Treatment = TreatmentTextBox.Text;

            return editedInfo;


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PatientRegisterForm patientRegisterForm = new PatientRegisterForm(mode);
            patientRegisterForm.Show();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = DialogResult.Cancel;

            if (mode == ModalMode.Add)
            {
                confirm = MessageBox.Show($"Are you sure you want to go back? Your progress will be lost.", "Back to Page 1", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
           
            if (confirm == DialogResult.Yes || mode == ModalMode.Edit)
            {
                try
                {
                    if (mode == ModalMode.Add)
                    {
                        Database.DeletePatientReg1Data(patient);
                    }

                   
                    var patientInfoForm = new PatientRegisterForm(mode, patient.AccountID)
                    {
                        username = patient.UserName, password = patient.Password
                    };
                 

                    patientInfoForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void FillUpFields()
        {
            HeightTextBox.Text = patient.HealthInfo.Height.ToString();
            WeightTextBox.Text = patient.HealthInfo.Weight.ToString();
            BmiTextBox.Text = patient.HealthInfo.BMI.ToString();
            BloodTypeComboBox.SelectedItem = patient.HealthInfo.BloodType == "" ? "Select BloodType" : patient.HealthInfo.BloodType;
            AlergyTextbox.Text = patient.HealthInfo.Alergy;
            MedicationTxtbox.Text = patient.HealthInfo.Medication;
            PreviousSurgeryTextBox.Text = patient.HealthInfo.PrevSurg;
            preConditionTextBox.Text = patient.HealthInfo.PreCon;
            TreatmentTextBox.Text = patient.HealthInfo.Treatment;
        }

        private void PatientBasicInformationForm_Load_1(object sender, EventArgs e)
        {
            LoadPatientName();

            if (mode == ModalMode.Edit)
            {
                FillUpFields();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult isDelete = MessageBox.Show("Are you sure you want to delete this information?", "Patient Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (isDelete == DialogResult.No) return;
            DeletePatientAndReload.Invoke();
            this.Hide();
            
        }
    }
}
