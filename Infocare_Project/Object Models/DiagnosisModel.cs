using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models
{
    /// <summary>
    /// DiagnosisModel Class that use to store Diagnostics Details.
    /// </summary>
    public class DiagnosisModel
    {
        public string Diagnosis { get; set; }
        public string AdditionalNotes { get; set; }
        public string DoctorOrders { get; set; }
        public string Prescription { get; set; }
    }
}
