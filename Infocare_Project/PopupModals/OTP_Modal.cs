using Guna.UI2.WinForms;
using Infocare_Project_1.Object_Models;
using OtpNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project_1.PopupModals
{
    public partial class OTP_Modal : Form
    {
        public Action<string> SavePass;
        string otp;
        Totp totp;
        string email;

        Guna2TextBox[] verifyBoxes;

        string[] rawOTP = new string[6];
        Form emailInput;

        public OTP_Modal(Totp totp, Form emailInput, string email)
        {
            this.totp = totp;
            this.emailInput = emailInput;
            InitializeComponent();
            SubscribeTextChanged();

            Guna2TextBox[] verifyBoxes = { verifyBox1, verifyBox2, verifyBox3, verifyBox4, verifyBox5, verifyBox6 };
            this.verifyBoxes = verifyBoxes;
            this.email = email;
        }

        private void SubscribeTextChanged()
        {
            verifyBox1.TextChanged += VerifyBoxesTextChanged;
            verifyBox2.TextChanged += VerifyBoxesTextChanged;
            verifyBox3.TextChanged += VerifyBoxesTextChanged;
            verifyBox4.TextChanged += VerifyBoxesTextChanged;
            verifyBox5.TextChanged += VerifyBoxesTextChanged;
            verifyBox6.TextChanged += VerifyBoxesTextChanged;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {



        }

        private void VerifyBoxesTextChanged(object sender, EventArgs e)
        {
            Control? box = sender as Control;
            int index = int.Parse(box?.Tag.ToString());
            rawOTP[index] = box.Text;

            if (box.Text != "" && index < 5)
            {
                verifyBoxes[index + 1].Focus();
            }
            submitBtn.Enabled = rawOTP.All(text => text != null && text.Trim() != "");
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            string inputOtp = String.Join("", rawOTP);
            Debug.WriteLine($"Input OTP: {inputOtp}");


            if (ProcessMethods.ValidateOTP(inputOtp, totp))
            {
                ResetPassword resetModal = new ResetPassword(emailInput, this);
                resetModal.SavePass += SavePass;
                resetModal.ShowDialog();
            }


            //ResetPassword resetPass = new ResetPassword();
            //resetPass.SavePass += SavePass;
        }

        private void OTP_Modal_Load(object sender, EventArgs e)
        {
            EmailTextBasis.Text = email;
        }
    }
}
