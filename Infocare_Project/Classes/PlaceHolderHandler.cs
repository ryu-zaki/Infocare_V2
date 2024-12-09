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
    }
}
