using Infocare_Project_1.Object_Models.Interfaces;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project_1.Object_Models
{
    public class AddressModel : IAddress
    {
        public int HouseNo { get; set; }
        public string Street { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int Zone { get; set; }

        string fullAddress;

        public string FullAddress
        {
            get
            {
                fullAddress = $"{HouseNo},{ZipCode}, {Zone}, {Street} street, Brgy. {Barangay}, {City}";
                return fullAddress;
            }

            set
            {
                fullAddress = value;
            }
        }
    }
}
