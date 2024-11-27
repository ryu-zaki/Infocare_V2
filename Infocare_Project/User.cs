using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project
{
    public class User
    {
        //info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Gender { get; set; }
        public string Suffix { get; set; }
        public string Bdate { get; set; }

        //accounts

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // Default constructor
        public static User u;
        public User() { u = this; }

        // Parameterized constructor for common user properties
        public User(string firstName, string lastName, string contactNumber, string gender, string suffix, string username, string password, string confirmPassword, string bdate)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            Gender = gender;
            Suffix = suffix;
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;  
            Bdate = bdate;
        }
    }

    public class Patient : User
    {
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }

        // Default constructor
        public static Patient p;
        public Patient() { p = this; }

        // Parameterized constructor for Patient-specific properties
        public Patient(string firstName, string lastName, string middleName, string contactNumber, DateTime birthDate, string gender)
        {
            // Manually initialize inherited properties
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ContactNumber = contactNumber;
            this.Gender = gender;

            // Initialize Patient-specific properties
            this.MiddleName = middleName;
            this.BirthDate = birthDate;
        }
    }

    public class Doctor : User
    {
        public string Specialty { get; set; }
        public double ConsultationFee { get; set; }

        // Default constructor
        public Doctor() { }

        // Parameterized constructor for Doctor-specific properties
        public Doctor(string firstName, string lastName, string contactNumber, string gender, string specialty, double consultationFee)
        {
            // Manually initialize inherited properties
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ContactNumber = contactNumber;
            this.Gender = gender;

            // Initialize Doctor-specific properties
            this.Specialty = specialty;
            this.ConsultationFee = consultationFee;
        }
    }

    public class Admin : User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Default constructor
        public Admin() { }

        // Parameterized constructor for Admin-specific properties
        public Admin(string firstName, string lastName, string contactNumber, string gender, string username, string password)
        {
            // Manually initialize inherited properties
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ContactNumber = contactNumber;
            this.Gender = gender;

            // Initialize Admin-specific properties
            this.Username = username;
            this.Password = password;
        }
    }

}
