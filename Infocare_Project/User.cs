using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project
{
    // Base class for User information
    public class User
    {
        // Info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string ContactNumber { get; set; }
        public string Sex { get; set; }
        public string Suffix { get; set; }
        public DateTime Bdate { get; set; }

        // Account info
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // Default constructor
        public User() { }

        // Parameterized constructor for common user properties
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
    }

    // Patient class inheriting from User
    public class Patient : User
    {
        // No need to add BirthDate here, as it is already in the base User class

        // Default constructor
        public Patient() { }

        // Parameterized constructor for Patient-specific properties
        public Patient(string firstName, string lastName, string middleName, string contactNumber, string sex, string suffix, string username, string password, string confirmPassword, DateTime bdate)
            : base(firstName, lastName, middleName, contactNumber, sex, suffix, username, password, confirmPassword, bdate)
        {
            // Patient-specific initialization (if needed)
        }
    }

    // Doctor class inheriting from User
    public class Doctor : User
    {
        public string Specialty { get; set; }
        public double ConsultationFee { get; set; }

        // Default constructor
        public Doctor() { }

        // Parameterized constructor for Doctor-specific properties
        public Doctor(string firstName, string lastName, string contactNumber, string sex, string specialty, double consultationFee, string username, string password)
            : base(firstName, lastName, string.Empty, contactNumber, sex, string.Empty, username, password, string.Empty, DateTime.MinValue)
        {
            Specialty = specialty;
            ConsultationFee = consultationFee;
        }
    }

    // Admin class inheriting from User
    public class Admin : User
    {
        // Default constructor
        public Admin() { }

        // Parameterized constructor for Admin-specific properties
        public Admin(string firstName, string lastName, string contactNumber, string sex, string username, string password)
            : base(firstName, lastName, string.Empty, contactNumber, sex, string.Empty, username, password, string.Empty, DateTime.MinValue)
        {
        }
    }
}
