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

                public User() { }

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

            public class Patient : User
            {

                public Patient() { }

                public Patient(string firstName, string lastName, string middleName, string contactNumber, string sex, string suffix, string username, string password, string confirmPassword, DateTime bdate)
                    : base(firstName, lastName, middleName, contactNumber, sex, suffix, username, password, confirmPassword, bdate)
                {
                }
            }

            //Doctor
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

            //Admin
            public class Admin : User
            {
                public Admin() { }

                public Admin(string firstName, string lastName, string contactNumber, string sex, string username, string password)
                    : base(firstName, lastName, string.Empty, contactNumber, sex, string.Empty, username, password, string.Empty, DateTime.MinValue)
                {
                }
            }
        }
