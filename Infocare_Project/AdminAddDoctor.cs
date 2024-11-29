using System;
using System.Windows.Forms;
using Infocare_Project.NewFolder;
using static Infocare_Project.NewFolder.PlaceHolderHandler;
using static Infocare_Project.NewFolder.Specialization;

namespace Infocare_Project
{
    public partial class AdminAddDoctor : Form
    {
        private PlaceHolderHandler _placeHolderHandler;

        public AdminAddDoctor()
        {
            InitializeComponent();
            _placeHolderHandler = new PlaceHolderHandler();
            // Instantiate the DoctorSpecialization class
            DoctorSpecialization doctorSpecialization = new DoctorSpecialization();

            // Set the data source for the ComboBox
            SpecializationComboBox.DataSource = doctorSpecialization.Specializations;

            // Configure the ComboBox to be searchable
            doctorSpecialization.ConfigureSearchableComboBox(SpecializationComboBox);
        }

        private void FirstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(FirstNameTextBox, FNLabel, "First name");
        }

        private void LastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(LastNameTextBox, LNLabel, "Last name");
        }

        private void UserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(UserNameTextBox, UNlabel, "User name");
        }

        private void ConsultationTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ConsultationTextBox, CFLabel, "Consultation fee");
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(PasswordTextBox, PLabel, "Password");
        }

        private void ConfirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ConfirmPasswordTextBox, CPLabel, "Confirm Password");
        }

        private void AdminAddDoctor_Load(object sender, EventArgs e)
        {
            TimeHelper.ChooseTime(TimeComboBox);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
