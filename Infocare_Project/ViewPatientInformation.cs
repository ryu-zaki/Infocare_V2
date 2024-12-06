using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class ViewPatientInformation : Form
    {
        public ViewPatientInformation()
        {
            InitializeComponent();
        }

        public ViewPatientInformation(string firstName, string lastName, string birthday, string preCondition, string medication,
                                      string treatment, string previousSurgery, string allergy, string bloodType, string bmi,
                                      string weight, string height, string diagnosis, string additionalNotes, string doctorOrder, string prescription)
        {
            InitializeComponent();

            // Populate the textboxes with the received data
            FirstNameTextBox.Text = firstName;
            LastNameTextBox.Text = lastName;
            BirthdayTextBox.Text = birthday;
            PreConditionTextBox.Text = preCondition;
            MedicationTextBox.Text = medication;
            TreatmentTextBox.Text = treatment;
            PreviousSurgeryTextBox.Text = previousSurgery;
            AllergyTextBox.Text = allergy;
            BloodTypeTextBox.Text = bloodType;
            BMITextBox.Text = bmi;
            WeightTextBox.Text = weight;
            HeightTextBox.Text = height;
            DiagnosisTextBox.Text = diagnosis;
            AdditionalNotesTextBox.Text = additionalNotes;
            DoctorOrderTextBox.Text = doctorOrder;
            PrescriptionTextBox.Text = prescription;
        }
    }
}
