using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models
{
    /// <summary>
    /// Appointment Class that use to store Appointment Details.
    /// </summary>
    public class Appointment
    {
        public string PatientName { get; set; }
        AppointmentStatus Status { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }

        public decimal ConsultationFee { get; set; }
        public DateTime Birthdate { get; set; }

        public HealthInfoModel healthInfo { get; set; }
        public DiagnosisModel Diagnosis { get; set; }


    }
}
