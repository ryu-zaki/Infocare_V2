using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project.NewFolder
{
    public class PlaceHolderHandler
    {
        public void HandleTextBoxPlaceholder(Guna2TextBox textBox, Guna2HtmlLabel label, string placeholderText)
        {
            if (textBox == null || label == null) throw new ArgumentNullException("TextBox or label cannot be null.");

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                // If the text box is empty, hide the label and show the placeholder text
                label.Visible = false;
                textBox.PlaceholderText = placeholderText;
            }
            else
            {
                // If the text box is not empty, show the label and clear the placeholder text
                label.Visible = true;
                textBox.PlaceholderText = string.Empty;
            }
        }

        public static class TimeHelper
        {
            public static void ChooseTime(ComboBox TimeComboBox)
            {
                TimeSpan startTime = new TimeSpan(8, 0, 0);
                TimeSpan endTime = new TimeSpan(20, 0, 0);
                TimeSpan DifferenceTime = new TimeSpan(4, 0, 0);

                for (TimeSpan time = startTime; time < endTime; time += DifferenceTime)
                {
                    TimeSpan nextTime = time + DifferenceTime;
                    string timeString = $"{DateTime.Today.Add(time):HH:mm} - {DateTime.Today.Add(nextTime):HH:mm}";
                    TimeComboBox.Items.Add(timeString);

                    if (nextTime >= endTime)
                        break;
                }

                if (TimeComboBox.Items.Count > 0)
                    TimeComboBox.SelectedIndex = 0;
            }

        }
    }
}
