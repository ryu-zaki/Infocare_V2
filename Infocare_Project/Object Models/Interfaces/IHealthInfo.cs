using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models.Interfaces
{
    internal interface IHealthInfo
    {
        double Height { get; set; }
        double Weight { get; set; }
        double BMI { get; set; }
        string BloodType { get; set; }
        string PreCon { get; set; }
        string Treatment { get; set; }
        string PrevSurg { get; set; }
        string Alergy { get; set; }
        string Medication { get; set; }
    }
}
