using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project
{
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
        public string HouseNo { get; set; }
        public string Street { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }

        // Default Constructor
        public User() { }

        // Parameterized Constructor (without address)
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

        // Parameterized Constructor (with address)
        public User(string firstName, string lastName, string middleName, string contactNumber, string sex, string suffix, string username, string password, string confirmPassword, DateTime bdate, string houseNo, string street, string barangay, string city)
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
            Street = street;
            Barangay = barangay;
            City = city;
        }
    }


    public class Patient : User
    {
        // BasicHealthInfo
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
        public string BloodType { get; set; }  // Fixed typo
        public string PreCon { get; set; }
        public string Treatment { get; set; }
        public string PrevSurg { get; set; }
        public string NameLbl { get; set; }

        public Patient() { }

        // Patient Constructor
        public Patient(string firstName, string lastName, string middleName, string contactNumber, string sex, string suffix, string username, string password, string confirmPassword, DateTime bdate,
                       double height, double weight, double bmi, string bloodType, string preCon, string treatment, string prevSurg, string nameLbl)
            : base(firstName, lastName, middleName, contactNumber, sex, suffix, username, password, confirmPassword, bdate)
        {
            Height = height;
            Weight = weight;
            BMI = bmi;
            BloodType = bloodType;
            PreCon = preCon;
            Treatment = treatment;
            PrevSurg = prevSurg;
            NameLbl = nameLbl;
        }
    }

    public class Doctor : User
    {
        public string Specialty { get; set; }
        public double ConsultationFee { get; set; }

        public Doctor() { }

        public Doctor(string firstName, string lastName, string contactNumber, string sex, string specialty, double consultationFee, string username, string password)
            : base(firstName, lastName, string.Empty, contactNumber, sex, string.Empty, username, password, string.Empty, DateTime.MinValue)
        {
            Specialty = specialty;
            ConsultationFee = consultationFee;
        }
    }

    public class Admin : User
    {
        public Admin() { }

        public Admin(string firstName, string lastName, string contactNumber, string sex, string username, string password)
            : base(firstName, lastName, string.Empty, contactNumber, sex, string.Empty, username, password, string.Empty, DateTime.MinValue)
        {
        }
    }
}
