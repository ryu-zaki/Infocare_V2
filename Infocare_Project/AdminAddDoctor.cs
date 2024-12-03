using System;
using System.Globalization;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Policy;
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

        }

        private void AdminAddDoctor_Load(object sender, EventArgs e)
        {
            TimeCombobox();
            DayAvComboBox();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(LastNameTextbox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(UserNameTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(ConfirmPasswordTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(SpecializationTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(ConsultationFeeTextBox.Text.Trim()) ||

                TimeComboBox.SelectedIndex == 0 || 
                DayAvailabilityCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Please fill out all fields and select valid options.");
                return;
            }

            Doctor newDoctor = new Doctor
            {
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextbox.Text.Trim(),
                MiddleName = MiddleNameTextbox.Text.Trim(),
                Username = UserNameTextBox.Text.Trim(),
                Password = PasswordTextBox.Text.Trim(),
                ConfirmPassword = ConfirmPasswordTextBox.Text.Trim(),
                ConsultationFee = int.TryParse(ConsultationFeeTextBox.Text, out int consultationFee) ? consultationFee : 0,
                Specialty = SpecializationTextBox.Text.Trim(),
            };

            string selectedTimeSlot = TimeComboBox.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(selectedTimeSlot) && selectedTimeSlot != "Select a time slot")
            {
                string[] timeSlots = selectedTimeSlot.Split('-');
                if (timeSlots.Length == 2)
                {
                    string startTimeString = timeSlots[0].Trim();
                    string endTimeString = timeSlots[1].Trim();

                    if (DateTime.TryParseExact(startTimeString, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startTime) &&
                        DateTime.TryParseExact(endTimeString, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endTime))
                    {
                        newDoctor.StartTime = startTime.TimeOfDay;
                        newDoctor.EndTime = endTime.TimeOfDay;
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid time.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid time slot.");
                    return;
                }
            }

            newDoctor.DayAvailability = DayAvailabilityCombobox.SelectedItem?.ToString() ?? string.Empty;

            if (newDoctor.Password != newDoctor.ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                Database db = new Database();
                db.AddDoctor(newDoctor);
                MessageBox.Show("Doctor added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }





        private void TimeCombobox()
        {
            TimeComboBox.Items.Clear();
            TimeComboBox.Items.Add("Select a time slot");

            TimeSpan startTime = TimeSpan.FromHours(8);
            TimeSpan endTime = TimeSpan.FromHours(20); 
            TimeSpan interval = TimeSpan.FromHours(4); 

            for (TimeSpan time = startTime; time < endTime; time += interval)
            {
                TimeSpan slotEndTime = time + interval;

                string formattedTime = $"{(DateTime.Today + time):hh:mm tt} - {(DateTime.Today + slotEndTime):hh:mm tt}";

                TimeComboBox.Items.Add(formattedTime);
            }

            TimeComboBox.SelectedIndex = 0;
        }


        private void DayAvComboBox()
        {
            DayAvailabilityCombobox.Items.Clear();
            DayAvailabilityCombobox.Items.Add("Select weekly schedule");

            DayAvailabilityCombobox.Items.Add("Monday-Wednesday-Friday");
            DayAvailabilityCombobox.Items.Add("Tuesday-Thursday-Saturday");

            DayAvailabilityCombobox.SelectedIndex = 0;
        }

        

  

        private void FirstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(FirstNameTextBox, FNLabel, "First name");
        }

        private void LastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(LastNameTextbox, LNLabel, "Last name");
        }

        private void UserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(UserNameTextBox, UNlabel, "User name");
        }

      

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(PasswordTextBox, PLabel, "Password");
        }

        private void ConfirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ConfirmPasswordTextBox, CPLabel, "Confirm Password");
        }
    }
}
