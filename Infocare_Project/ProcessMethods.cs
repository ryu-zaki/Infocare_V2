using FluentEmail.Core;
using FluentEmail.Smtp;
using Guna.UI2.WinForms;
using Infocare_Project_1.Object_Models;
using Infocare_Project_1.PopupModals;
using OtpNet;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infocare_Project_1
{
    public static  class ProcessMethods
    {
        public static bool ValidatePassword(string password)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$");

            if (!regex.IsMatch(password))
            {
                MessageBox.Show("Password must be at least 8 characters long and include:\n- At least one uppercase letter\n- At least one lowercase letter\n- At least one number\n- At least one special character (e.g., @, !, etc.)",
                                "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public static bool IsValidTextInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            if (input.Equals("N/A", StringComparison.OrdinalIgnoreCase))
                return true;

            return input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }


        public static string HashCharacter(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);

                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static void SendEmail()
        {

        }

        public static void viewForgotPass(Role role)
        {
            EmailUsernameInput inputForm = new EmailUsernameInput(role);
            inputForm.ShowDialog();
        }

        public static string GetTablenameByRole(Role role)
        {
            string tableName =
                      role == Role.Staff ? "tb_staffinfo" :
                       role == Role.Admin ? "tb_adminlogin" :
                          role == Role.Doctor ? "tb_doctorinfo" : "";
            return tableName;
        }

        public static bool ValidateFields(Guna2TextBox[] fields)
        { 
              foreach( Guna2TextBox textBox in fields)
               {
                   if (textBox.Text.Trim() == "")
                   {
                    textBox.FocusedState.BorderColor = Color.Red;
                    textBox.BorderColor = Color.Red;

                    return false;
                    } 
               }

           
            return true;
        }

        public static string GenerateOTP(Totp totp)
        {

            string otp = totp.ComputeTotp();
         
            return otp;
        }

        public static bool ValidateOTP(string inputOTP, Totp totp)
        {

            return totp.VerifyTotp(inputOTP, out _, new VerificationWindow(previous: 1, future: 1));
        }


        public static async void SendEmail(UserModel user, string otp)
        {
            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("infocare004@gmail.com", "scde knkt wrfy qfzt"),
                EnableSsl = true,
            };

            Email.DefaultSender = new SmtpSender(client);

            var engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "EmailTemplates"))
            .UseMemoryCachingProvider()
            .Build();

            var data = new
            {
                Name = user.UserName,
                OTP = otp
            };

            string razorTemplate = "otp.cshtml";
            string body = await engine.CompileRenderAsync(razorTemplate, data);

            var email = Email
                .From("infocare004@gmail.com", "InfoCare")
                .To(user.Email)
                .Subject("Your One-Time Password (OTP)")
                .Body(body, true);

            var response = await email.SendAsync();

            if (response.Successful)
            {
                 Debug.WriteLine("OTP email sent successfully!");
            }
            else
            {
                Debug.WriteLine("Failed to send OTP email: " + string.Join(", ", response.ErrorMessages));
            }
        }

        public static bool ValidateEmail(string inputEmail)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(inputEmail, pattern);
        }

    } 
}
