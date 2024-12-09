using Infocare_Project_1.Object_Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models
{
    /// <summary>
    /// DiagnEmergencyContactModel Class that use to store Emergency Contact Details of a Patient.
    /// </summary>
    public class EmergencyContactModel : IEmergencyContact
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        
        public AddressModel address { get; set; }

    }


}
