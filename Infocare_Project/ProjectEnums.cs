using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1
{
    public enum AppointmentStatus
    {
        InvoiceChecked,
        Pending,
        Accepted,
        Completed
    }

    public enum Sex
    {
        Male,
        Female
    }


    public enum Role 
    {
        Admin,
        Staff,
        Doctor,
        Patient
    }

    public enum ModalMode 
    {
        Add,
        Edit,
        PreserveInfo
    }

    public enum PanelMode
    {
        AdminDoc,
        Patient
    }


}
