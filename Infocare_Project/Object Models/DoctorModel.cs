using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models
{
    /// <summary>
    /// DiagnosticModel Class that use to store all the details about the doctor.
    /// </summary>
    public class DoctorModel : UserModel
    {
        public List<string> Specialty { get; set; }
        public decimal ConsultationFee { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayAvailability { get; set; }

        
    }
}
