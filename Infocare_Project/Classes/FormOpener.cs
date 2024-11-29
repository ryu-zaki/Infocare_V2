using System;
using System.Windows.Forms;

namespace Infocare_Project.Classes
{
    public static class FormOpener
    {
        public static void OpenForm(string formName)
        {
            // Check if the form is already open
            Form existingForm = Application.OpenForms[formName];
            if (existingForm == null)
            {
                // If the form isn't already open, create and show it
                try
                {
                    // Create an instance of the form using the form's name
                    Type formType = Type.GetType(formName);
                    if (formType != null)
                    {
                        Form newForm = (Form)Activator.CreateInstance(formType);
                        newForm.Show();
                    }
                    else
                    {
                        // Show an error message if the form type could not be found
                        MessageBox.Show($"Form {formName} could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Show an error message if an exception occurs while creating the form
                    MessageBox.Show($"An error occurred while opening the form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // If the form is already open, bring it to the front and focus it
                existingForm.BringToFront();
                existingForm.Focus();
            }
        }
    }
}
