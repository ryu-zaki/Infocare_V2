using Infocare_Project_1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models
{

    /// <summary>
    /// PatientModel Class that use to store all the Details of a Patient.
    /// </summary>

    public class PatientModel : UserModel
    {
        public int AccountID { get; set; }
        public HealthInfoModel HealthInfo { get; set; }
        public EmergencyContactModel EmergencyContact { get; set; }
        public DateTime BirthDate { get; set; }
        public string sex { get; set; }
        public string Suffix { get; set; }

        public AddressModel Address { get; set; }
       
    }
}
