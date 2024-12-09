using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models.Interfaces
{
    internal interface IAddress
    {
        int HouseNo { get; set; }
        string Street { get; set; }
        string Barangay { get; set; }
        string City { get; set; }
        int ZipCode { get; set; }
        int Zone { get; set; }

        public string FullAddress
        {
            get
            {
                return $"{HouseNo},{ZipCode}, {Zone}, {Street} street, Brgy. {Barangay}, {City}";
            }

            set
            {
                FullAddress = value;
            }
        }
    }
}
