using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Infocare_Project_1.Object_Models
{
    /// <summary>
    /// StaffModel Class that use to store all the details about the staff.
    /// </summary>
    public class StaffModel : UserModel
    {
        public string Suffix { get; set; } 
        public int AccountID { get; set; }
       
    }
}
