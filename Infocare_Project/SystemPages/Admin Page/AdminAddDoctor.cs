﻿using System;
using System.Globalization;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Infocare_Project.NewFolder;
using Infocare_Project_1;
using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models;
using static Infocare_Project.NewFolder.PlaceHolderHandler;
using static Infocare_Project.NewFolder.Specialization;

namespace Infocare_Project
{
    public partial class AdminAddDoctor : Form
    {
        private PlaceHolderHandler _placeHolderHandler;
        public Action ShowDoctorList;
        bool passShow;
        ModalMode mode;
        int AccountId;
        DoctorModel doctor;

        public AdminAddDoctor(ModalMode mode = ModalMode.Add, int AccountID = 0)
        {
            InitializeComponent();
            _placeHolderHandler = new PlaceHolderHandler();

            this.mode = mode;
            this.AccountId = AccountID;

            if (mode == ModalMode.Edit)
            {
                RegisterButton.Text = "Update";
                guna2HtmlLabel2.Text = "Update Doctor";
                PasswordTextBox.Visible = false;
                ConfirmPasswordTextBox.Visible = false;
                removeDoctor.Visible = true;
                passValidatorMsg.Visible = false;
            }
        }

        public String ConcatenateTimeSpan(TimeSpan startTime, TimeSpan endTime)
        {
            DateTime start = DateTime.Today.Add(startTime);
            DateTime end = DateTime.Today.Add(endTime);

            return $"{start.ToString("hh:mm tt")} - {end.ToString("hh:mm tt")}";
        }

        public void FillUpFields(DoctorModel doctor)
        {
            FirstNameTextBox.Text = doctor.FirstName;
            ConsultationFeeTextBox.Text = doctor.ConsultationFee.ToString();
            LastNameTextbox.Text = doctor.LastName;
            MiddleNameTextbox.Text = doctor.MiddleName;
            DayAvailabilityCombobox.SelectedItem = doctor.DayAvailability;

            UserNameTextBox.Text = doctor.UserName;
            emailTextBox.Text = doctor.Email;
            ContactNumberTextbox.Text = doctor.ContactNumber;



            TimeCombobox();
            TimeComboBox.SelectedItem = ConcatenateTimeSpan(doctor.StartTime, doctor.EndTime);

            flowLayoutPanel1.Controls.Clear();
            foreach (string skill in doctor.Specialty)
            {
                Guna2TextBox label = new Guna2TextBox();
                label.Text = skill;

                flowLayoutPanel1.Controls.Add(label);
            }


        }

        private void AdminAddDoctor_Load(object sender, EventArgs e)
        {
            TimeCombobox();
            DayAvComboBox();

            if (mode == ModalMode.Edit)
            {
                doctor = Database.GetDoctorInfo(AccountId);
                FillUpFields(doctor);
            }

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            // Validate required fields

            string contactNumber = ContactNumberTextbox.Text;

            if (contactNumber.Length > 0 && (contactNumber.Length != 11 || !contactNumber.StartsWith("09") || !contactNumber.All(char.IsDigit)))
            {
                MessageBox.Show("Invalid number. The contact number must start with '09' and be exactly 11 digits.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!FirstNameTextBox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("First name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!LastNameTextbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && !string.IsNullOrEmpty(LastNameTextbox.Text))
            {
                MessageBox.Show("First name must contain only letters and spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!MiddleNameTextbox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '/') && !string.IsNullOrEmpty(MiddleNameTextbox.Text))
            {
                MessageBox.Show("Middle name must contain only letters, spaces, and the '/' character.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (Database.UsernameExistsDoctor(UserNameTextBox.Text) && mode == ModalMode.Add)
            {
                MessageBox.Show("The username is already in use. Please choose a different username.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string password = PasswordTextBox.Text;

            if (mode == ModalMode.Add)
            {
                if (!ProcessMethods.ValidatePassword(password))
                {
                    return;
                }
            }


            if (!ProcessMethods.ValidateEmail(emailTextBox.Text))
            {
                MessageBox.Show("Please enter a valid email.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(LastNameTextbox.Text.Trim()) ||
                string.IsNullOrWhiteSpace(UserNameTextBox.Text.Trim()) ||
                (string.IsNullOrWhiteSpace(PasswordTextBox.Text.Trim()) && mode == ModalMode.Add) ||
                (string.IsNullOrWhiteSpace(ConfirmPasswordTextBox.Text.Trim()) && mode == ModalMode.Add) ||
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
            decimal Contact;

            if (!decimal.TryParse(ConsultationFeeTextBox.Text, out Contact))
            {
                MessageBox.Show("Please enter a valid Consultation Fee.");
                return;
            }

            DoctorModel newDoctorInfo = new DoctorModel
            {
                AccountID = mode == ModalMode.Edit ? doctor.AccountID : 0,
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextbox.Text.Trim(),
                MiddleName = MiddleNameTextbox.Text.Trim(),
                ContactNumber = ContactNumberTextbox.Text.Trim(),
                UserName = UserNameTextBox.Text.Trim(),
                Password = mode == ModalMode.Edit ? doctor.Password : PasswordTextBox.Text.Trim(),
                Email = emailTextBox.Text,

                ConsultationFee = decimal.TryParse(ConsultationFeeTextBox.Text, out decimal consultationFee) ? consultationFee : 0,
                Specialty = specializations,
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
                        newDoctorInfo.StartTime = startTime.TimeOfDay;
                        newDoctorInfo.EndTime = endTime.TimeOfDay;
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

            newDoctorInfo.DayAvailability = DayAvailabilityCombobox.SelectedItem?.ToString() ?? string.Empty;

            if (PasswordTextBox.Text.Trim() != ConfirmPasswordTextBox.Text.Trim())
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                int doctorId = Database.AddUpdateDoctor1(newDoctorInfo, mode);

                foreach (string specialization in specializations)
                {
                    Database.AddSpecialization(doctorId, specialization);
                }


                MessageBox.Show($"Doctor {(mode == ModalMode.Add ? "Added" : "Info Updated")} successfully!");
                this.Hide();
                ShowDoctorList.Invoke();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }



        private void TimeCombobox(ModalMode mode = ModalMode.Add)
        {
            TimeComboBox.Items.Clear();
            TimeComboBox.Items.Add("Select a time slot");

            TimeSpan startTime = mode == ModalMode.Add ? TimeSpan.FromHours(8) : doctor.StartTime;
            TimeSpan endTime = mode == ModalMode.Add ? TimeSpan.FromHours(20) : doctor.EndTime;
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


            if (PasswordTextBox.Text.Trim() == "")
            {
                passValidatorMsg.Visible = false;
            }
            else
            {
                passValidatorMsg.Visible = true;
                string msg =
                !Regex.IsMatch(PasswordTextBox.Text, @"[A-Z]") ? "Add at least one uppercase letter" :
                !Regex.IsMatch(PasswordTextBox.Text, @"[^a-zA-Z0-9\s]") ? "Add At least one special character" : !Regex.IsMatch(PasswordTextBox.Text, @"[\d]") ? "Add At least one number" : !Regex.IsMatch(PasswordTextBox.Text, @".{8,}") ? "Must have at least 8 characters long" : "";

                if (msg == "")
                {

                    passValidatorMsg.Text = "*Strong Enough";
                    passValidatorMsg.ForeColor = Color.Green;
                }
                else
                {
                    passValidatorMsg.Text = "*" + msg;
                    passValidatorMsg.ForeColor = Color.Red;

                }

            }
        }

        private void ConfirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            _placeHolderHandler.HandleTextBoxPlaceholder(ConfirmPasswordTextBox, CPLabel, "Confirm Password");
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (mode == ModalMode.Add)
            {
                DialogResult confirm = MessageBox.Show("Are you sure to cancel registration?", "Cancel registraion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)

                {
                    this.Close();
                }

                return;
            }

            this.Close();

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

        private void removeDoctor_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this doctor?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                Database.DeleteDoctorById(doctor.AccountID);

                ShowDoctorList.Invoke();
                this.Close();

            }

        }

        private void PasswordTextBox_IconRightClick(object sender, EventArgs e)
        {
            if (passShow)
            {
                PasswordTextBox.PasswordChar = '\0';
                ConfirmPasswordTextBox.PasswordChar = '\0';
                PasswordTextBox.IconRight = AdminDoctor_Panel.Properties.Resources.hide_password_logo;
                passShow = false;


            }
            else
            {
                PasswordTextBox.PasswordChar = '●';
                ConfirmPasswordTextBox.PasswordChar = '●';
                PasswordTextBox.IconRight = AdminDoctor_Panel.Properties.Resources.show_password_logo;
                passShow = true;
            }
        }
    }
}
