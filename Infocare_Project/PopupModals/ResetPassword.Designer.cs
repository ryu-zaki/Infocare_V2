﻿namespace Infocare_Project_1.Object_Models
{
    partial class ResetPassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            newpassTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            confirmpassTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            savePassBtn = new Guna.UI2.WinForms.Guna2Button();
            showPass = new Guna.UI2.WinForms.Guna2CheckBox();
            passValidatorMsg = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 10;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel1.Location = new Point(136, 32);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(243, 34);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Reset Your Password";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            guna2HtmlLabel2.Location = new Point(123, 72);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(267, 23);
            guna2HtmlLabel2.TabIndex = 1;
            guna2HtmlLabel2.Text = "Set the new password of your account";
            // 
            // newpassTextbox
            // 
            newpassTextbox.BorderColor = SystemColors.ControlDarkDark;
            newpassTextbox.BorderRadius = 5;
            newpassTextbox.CustomizableEdges = customizableEdges5;
            newpassTextbox.DefaultText = "";
            newpassTextbox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            newpassTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            newpassTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            newpassTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            newpassTextbox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            newpassTextbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            newpassTextbox.ForeColor = SystemColors.ActiveCaptionText;
            newpassTextbox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            newpassTextbox.Location = new Point(88, 131);
            newpassTextbox.Name = "newpassTextbox";
            newpassTextbox.PasswordChar = '●';
            newpassTextbox.PlaceholderForeColor = Color.FromArgb(64, 64, 64);
            newpassTextbox.PlaceholderText = "New Password";
            newpassTextbox.SelectedText = "";
            newpassTextbox.ShadowDecoration.CustomizableEdges = customizableEdges6;
            newpassTextbox.Size = new Size(336, 42);
            newpassTextbox.TabIndex = 2;
            newpassTextbox.TextChanged += newpassTextbox_TextChanged;
            // 
            // confirmpassTextbox
            // 
            confirmpassTextbox.BorderColor = SystemColors.ControlDarkDark;
            confirmpassTextbox.BorderRadius = 5;
            confirmpassTextbox.CustomizableEdges = customizableEdges3;
            confirmpassTextbox.DefaultText = "";
            confirmpassTextbox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            confirmpassTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            confirmpassTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            confirmpassTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            confirmpassTextbox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            confirmpassTextbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            confirmpassTextbox.ForeColor = SystemColors.ActiveCaptionText;
            confirmpassTextbox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            confirmpassTextbox.Location = new Point(88, 208);
            confirmpassTextbox.Name = "confirmpassTextbox";
            confirmpassTextbox.PasswordChar = '●';
            confirmpassTextbox.PlaceholderForeColor = Color.FromArgb(64, 64, 64);
            confirmpassTextbox.PlaceholderText = "Confirm Password";
            confirmpassTextbox.SelectedText = "";
            confirmpassTextbox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            confirmpassTextbox.Size = new Size(336, 42);
            confirmpassTextbox.TabIndex = 3;
            // 
            // savePassBtn
            // 
            savePassBtn.BackColor = Color.Transparent;
            savePassBtn.BorderRadius = 5;
            savePassBtn.CustomizableEdges = customizableEdges1;
            savePassBtn.DisabledState.BorderColor = Color.DarkGray;
            savePassBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            savePassBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            savePassBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            savePassBtn.FillColor = Color.MidnightBlue;
            savePassBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            savePassBtn.ForeColor = Color.White;
            savePassBtn.Location = new Point(160, 312);
            savePassBtn.Name = "savePassBtn";
            savePassBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            savePassBtn.Size = new Size(180, 45);
            savePassBtn.TabIndex = 4;
            savePassBtn.Text = "Save Password";
            savePassBtn.Click += savePassBtn_Click;
            // 
            // showPass
            // 
            showPass.AutoSize = true;
            showPass.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            showPass.CheckedState.BorderRadius = 0;
            showPass.CheckedState.BorderThickness = 0;
            showPass.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            showPass.Location = new Point(88, 272);
            showPass.Name = "showPass";
            showPass.Size = new Size(113, 19);
            showPass.TabIndex = 5;
            showPass.Text = "Show Passwords";
            showPass.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            showPass.UncheckedState.BorderRadius = 0;
            showPass.UncheckedState.BorderThickness = 0;
            showPass.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            showPass.CheckedChanged += showPass_CheckedChanged;
            // 
            // passValidatorMsg
            // 
            passValidatorMsg.BackColor = Color.Transparent;
            passValidatorMsg.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            passValidatorMsg.ForeColor = Color.Red;
            passValidatorMsg.Location = new Point(88, 178);
            passValidatorMsg.Margin = new Padding(3, 2, 3, 2);
            passValidatorMsg.Name = "passValidatorMsg";
            passValidatorMsg.Size = new Size(138, 15);
            passValidatorMsg.TabIndex = 195;
            passValidatorMsg.Text = "*At least 8 characters long";
            passValidatorMsg.Visible = false;
            // 
            // ResetPassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 394);
            Controls.Add(passValidatorMsg);
            Controls.Add(showPass);
            Controls.Add(savePassBtn);
            Controls.Add(confirmpassTextbox);
            Controls.Add(newpassTextbox);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(guna2HtmlLabel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ResetPassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ResetPassword";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2TextBox confirmpassTextbox;
        private Guna.UI2.WinForms.Guna2TextBox newpassTextbox;
        private Guna.UI2.WinForms.Guna2Button savePassBtn;
        private Guna.UI2.WinForms.Guna2CheckBox showPass;
        private Guna.UI2.WinForms.Guna2HtmlLabel passValidatorMsg;
    }
}