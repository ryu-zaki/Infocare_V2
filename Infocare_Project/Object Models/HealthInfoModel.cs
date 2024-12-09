using Infocare_Project_1.Object_Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models
{

    /// <summary>
    /// HealthInfoModel Class that use to store Health Related Details of a Patient.
    /// </summary>

    public class HealthInfoModel : IHealthInfo
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
        public string BloodType { get; set; }
        public string PreCon { get; set; }
        public string Treatment { get; set; }
        public string PrevSurg { get; set; }
        public string Alergy { get; set; }
        public string Medication { get; set; }
    }
}
