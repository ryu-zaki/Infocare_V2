using Infocare_Project_1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models.Interfaces
{
    public interface IPerson
    {
        // Personal Information
        string FirstName { get; set; }
        string LastName { get; set; }
        string UserName { get; set; }
        string MiddleName { get; set; }
        string Password { get; set; }
        string ContactNumber { get; set; }
        string Email { get; set; }
    }
}
