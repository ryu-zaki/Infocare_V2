using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Infocare_Project.NewFolder
{
    internal class Specialization
    {
        public class DoctorSpecialization
        {
            public List<string> Specializations { get; private set; }

            public DoctorSpecialization()
            {
                Specializations = new List<string>
                {
                    "Specialization","Cardiology", "Dermatology", "Neurology", "Orthopedics", "Pediatrics", "Psychiatry",
                    "Ophthalmology", "Obstetrics and Gynecology", "Endocrinology", "Gastroenterology",
                    "Hematology", "Oncology", "Pulmonology", "Nephrology", "Urology", "General Surgery",
                    "Plastic Surgery", "Radiology", "Anesthesiology", "Internal Medicine", "Family Medicine",
                    "Emergency Medicine", "Rheumatology", "Pathology", "Vascular Surgery", "Rehabilitation Medicine",
                    "Geriatrics", "Sports Medicine", "Infectious Disease", "Palliative Care"
                };
            }

            public void ConfigureSearchableComboBox(ComboBox comboBox)
            {
                // Set the ComboBox properties for appearance
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Set the text color and font
                comboBox.ForeColor = Color.FromArgb(47, 89, 114);
                comboBox.Font = new Font("Segoe UI", 9f);

                // Remove the border of the search bar
               // comboBox.BorderStyle = BorderStyle.None;
            }
        }
    }
}


