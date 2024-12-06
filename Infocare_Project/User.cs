using System;

namespace Infocare_Project
{
    // Emergency Contact Class
    public class EmergencyContact
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int HouseNo { get; set; }
        public string Street { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int Zone { get; set; }

        // Default Constructor
        public EmergencyContact() { }

        // Parameterized Constructor
        public EmergencyContact(string firstName, string lastName, string middleName, string suffix, int houseNo, string street, string barangay, string city, int zipCode, int zone)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Suffix = suffix;
            HouseNo = houseNo;
            Street = street;
            Barangay = barangay;
            City = city;
            ZipCode = zipCode;
            Zone = zone;
        }
    }

    // User Class
    public class User
    {
        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string ContactNumber { get; set; }
        public string Sex { get; set; }
        public string Suffix { get; set; }
        public DateTime Bdate { get; set; }

        // Account Information
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // Address Components
        public int HouseNo { get; set; }
        public string Street { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int Zone { get; set; }

        // Additional Information
        public string Email { get; set; } // Added Email property

        // Emergency Contact
        public EmergencyContact EmergencyContact { get; set; }

        // Default Constructor
        public User() { }

        // Parameterized Constructor (without emergency contact)
        public User(string firstName, string lastName, string middleName, string contactNumber, string sex, string suffix, string username, string password, string confirmPassword, DateTime bdate)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            ContactNumber = contactNumber;
            Sex = sex;
            Suffix = suffix;
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;
            Bdate = bdate;
        }

        // Parameterized Constructor (with address and emergency contact)
        public User(string firstName, string lastName, string middleName, string contactNumber, string sex, string suffix, string username, string password, string confirmPassword, DateTime bdate,
            int houseNo, int zipcode, int zone, string street, string barangay, string city, EmergencyContact emergencyContact)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            ContactNumber = contactNumber;
            Sex = sex;
            Suffix = suffix;
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;
            Bdate = bdate;
            HouseNo = houseNo;
            ZipCode = zipcode;
            Zone = zone;
            Street = street;
            Barangay = barangay;
            City = city;

            EmergencyContact = emergencyContact; // Assign emergency contact object
        }
    }

    // Patient Class
    public class Patient : User
    {
        // Basic Health Info
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
        public string BloodType { get; set; }
        public string PreCon { get; set; }
        public string Treatment { get; set; }
        public string PrevSurg { get; set; }
        public string NameLbl { get; set; }
        public string Alergy { get; set; }
        public string Medication { get; set; }

        public Patient() { }

        // Patient Constructor
        public Patient(string firstName, string lastName, string middleName, string contactNumber, string sex, string suffix, string username, string password, string confirmPassword, DateTime bdate,
                       double height, double weight, double bmi, string bloodType, string preCon, string treatment, string prevSurg, string nameLbl, string alergy, string medication, EmergencyContact emergencyContact)
            : base(firstName, lastName, middleName, contactNumber, sex, suffix, username, password, confirmPassword, bdate, 0, 0, 0, "", "", "", emergencyContact)
        {
            Height = height;
            Weight = weight;
            BMI = bmi;
            BloodType = bloodType;
            PreCon = preCon;
            Treatment = treatment;
            PrevSurg = prevSurg;
            NameLbl = nameLbl;
            Alergy = alergy;
            Medication = medication;
        }
    }

    // Doctor Class
    public class Doctor : User
    {
        public List<string> Specialty { get; set; }
        public decimal ConsultationFee { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayAvailability { get; set; }

        public Doctor() { }

        public Doctor(string firstName, string lastName, string contactNumber, string sex, List<string> specialty, decimal consultationFee, string username, string password, TimeSpan start_time, TimeSpan end_time, string dayAvailability)
            : base(firstName, lastName, string.Empty, contactNumber, sex, string.Empty, username, password, string.Empty, DateTime.MinValue)
        {
            Specialty = specialty;
            ConsultationFee = consultationFee;
            StartTime = start_time;
            EndTime = end_time;
            DayAvailability = dayAvailability;
        }
    }

    // Staff Class
    public class Staff : User
    {
        public Staff() { }

        public Staff(string firstName, string middleName, string lastName, string suffix, string email, string contactNumber, string username, string password)
            : base(firstName, lastName, middleName, contactNumber, string.Empty, suffix, username, password, string.Empty, DateTime.MinValue)
        {
            Email = email; // Correctly assigning to Email property
        }
    }

    // Admin Class
    public class Admin : User
    {
        public Admin() { }

        public Admin(string firstName, string lastName, string contactNumber, string sex, string username, string password)
            : base(firstName, lastName, string.Empty, contactNumber, sex, string.Empty, username, password, string.Empty, DateTime.MinValue)
        {
        }
    }
}
