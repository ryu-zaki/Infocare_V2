using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project
{
    class User
    {
        public static User u;
        public User() { u = this; }


        //Patient Fields
        public string patientID;
        public string patientFirstName;
        public string patientLastName;
        public string patientUserName;
        public string patientPassword;
        public string patientAge;
        public string patientContact;
        public string patientBloodType;
        public string patientGender;
        public string patientAddress;

        //Doctor Fields
        public string DocID;
        public string DocFirstName;
        public string DocLastName;
        public string DocUserName;
        public string DocPassword;
        public string DocAge;
        public string DocContact;
        public string DocGender;
        public string DocSpecialization;

        //Patient Properties
        public string PatientID
        {
            set { this.patientID = value;}
            get { return this.patientID; }
        }
        public string PatientFirstName
        {
            set { this.patientFirstName = value; }
            get { return this.patientFirstName; }
        }
        public string PatientLastName
        {
            set { this.patientLastName = value; }
            get { return this.patientLastName; }
        }
        public string PatientUserName
        {
            set { this.patientUserName = value; }
            get { return this.patientUserName; }
        }
        public string PatientPassword
        {
            set { this.patientPassword = value; } 
            get { return this.patientPassword; }
        }
        public string PatientAge
        {
            set { this.patientAge = value; }
            get
            {
                return this.patientAge;
            }
        }
        public string PatientContact
        {
            set { this.patientContact = value; }
            get { return this.patientUserName; }
        }
        public string PatientBloodType
        {
            set { this.patientBloodType = value; }
            get { return this.patientBloodType; }
        }
        public string PatientGender
        {
            set { this.patientGender = value; }
            get { return this.patientGender; }
        }
        public string PatientAddress
        {
            set { this.patientAddress = value; }
            get { return this.patientAddress; }
        }

        //Doctor Properties
        public string docID
        {
            set { this.DocID = value; }
            get { return this.DocID; }
        }
        public string docFirstName
        {
            set { this.DocFirstName = value; }
            get { return this.DocID; }
        }
        public string docLastName
        {
            set { this.DocID = value; }
            get { return this.DocID; }
        }
        public string docUserName
        {
            set { this.DocUserName = value; }
            get { return this.DocUserName; }
        }
        public string docPassword
        {
            set { this.DocPassword = value; }
            get { return this.DocPassword; }
        }
        public string docAge
        {
            set { this.DocAge = value; }
            get { return this.DocAge; }
        }
        public string docContact
        {
            set { this.DocContact = value; }
            get { return this.DocContact; }
        }
        public string docGender
        {
            set { this.DocGender = value; }
            get { return this.DocGender; }
        }
        public string docSpecialization
        {
            set { this.DocSpecialization = value; }
            get { return this.DocSpecialization; }
        }
        //
        public int logIn(string user, string pass)
        {
            Ty
        }
    }
}
