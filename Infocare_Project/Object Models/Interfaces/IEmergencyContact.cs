using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models.Interfaces
{
    internal interface IEmergencyContact
    {
         string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Suffix { get; set; }

        AddressModel address { get; set; }
    }
}
