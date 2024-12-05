using System;
using System.Globalization;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Windows.Forms;
using Guna.UI2.WinForms;
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
            // Validate required fields
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(LastNameTextbox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(UserNameTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(ConfirmPasswordTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(ConsultationFeeTextBox.Text.Trim()) ||
                TimeComboBox.SelectedIndex == 0 ||
                DayAvailabilityCombobox.SelectedIndex == 0)
            {
                MessageBox.Show("Please fill out all fields and select valid options.");
                return;
            }

            List<string> specializations = new List<string>();
            foreach (var control in flowLayoutPanel1.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2TextBox specializationTextBox)
                {
                    if (!string.IsNullOrWhiteSpace(specializationTextBox.Text.Trim()))
                    {
                        specializations.Add(specializationTextBox.Text.Trim());
                    }
                }
            }

            if (specializations.Count == 0)
            {
                MessageBox.Show("Please enter at least one specialization.");
                return;
            }


            // Create a new Doctor object
            Doctor newDoctor = new Doctor
            {
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextbox.Text.Trim(),
                MiddleName = MiddleNameTextbox.Text.Trim(),
                Username = UserNameTextBox.Text.Trim(),
                Password = PasswordTextBox.Text.Trim(),
                ConfirmPassword = ConfirmPasswordTextBox.Text.Trim(),
                ConsultationFee = int.TryParse(ConsultationFeeTextBox.Text, out int consultationFee) ? consultationFee : 0,
                Specialty = specializations, // Store the list of specializations
            };

            // Validate time slot
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
                int doctorId = db.AddDoctor(newDoctor);

                foreach (string specialization in specializations)
                {
                    db.AddSpecialization(doctorId, specialization);
                }

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

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure to cancel registration?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void BackButton_Click_1(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to go back? Unsaved changes will be lost.", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)

            {
                this.Close();
            }
        }

        private void AddSpecialization_Click(object sender, EventArgs e)
        {
            Guna2TextBox newSpecializationTextBox = new Guna2TextBox
            {
                Width = 194,
                Height = 38,
                Margin = new Padding(3, 3, 3, 3),
                PlaceholderText = "Specialization",
                PlaceholderForeColor = Color.FromArgb(47, 89, 114),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(47, 89, 114),
                BorderColor = Color.FromArgb(93, 202, 209),
                BorderThickness = 1,
                BorderRadius = 8,
                BackColor = Color.FromArgb(110, 177, 247),
                Cursor = Cursors.Default,
                Padding = new Padding(0),
                IconLeftSize = new Size(20, 20),
                IconRightSize = new Size(20, 20),
                WordWrap = true,
                Multiline = false,
                TextAlign = HorizontalAlignment.Left,
            }; 

            flowLayoutPanel1.Controls.Add(newSpecializationTextBox);
        }
    }
}
