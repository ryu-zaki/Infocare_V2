using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models
{

    /// <summary>
    ///   UserModel Class that use to store Diagnostics Details.
    /// </summary>

    public class UserModel : IPerson
    {
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string UserName { get; set; }
       public string MiddleName { get; set; }
       public string Password { get; set; }
       public string ContactNumber { get; set; }
        public string Email { get; set; }


    }
}
