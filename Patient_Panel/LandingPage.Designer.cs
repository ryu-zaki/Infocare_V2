namespace Patient_Panel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panel1 = new Panel();
            label1 = new Label();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            MinimizeButton = new Guna.UI2.WinForms.Guna2ImageButton();
            ExitButton = new Guna.UI2.WinForms.Guna2Button();
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
            guna2HtmlLabel1.Font = new Font("Inter ExtraBold", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlLightLight;
            guna2HtmlLabel1.Location = new Point(97, 115);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(500, 70);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Welcome to Infocare";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Location = new Point(193, 230);
            panel1.Name = "panel1";
            panel1.Size = new Size(301, 5);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Inter", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(220, 200);
            label1.Name = "label1";
            label1.Size = new Size(248, 27);
            label1.TabIndex = 2;
            label1.Text = "Your Health is our Priority";
            // 
            // guna2Button1
            // 
            guna2Button1.BackColor = Color.Transparent;
            guna2Button1.BorderColor = Color.RosyBrown;
            guna2Button1.BorderRadius = 30;
            guna2Button1.CustomizableEdges = customizableEdges4;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.FromArgb(110, 177, 247);
            guna2Button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(246, 289);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges5;
            guna2Button1.Size = new Size(180, 59);
            guna2Button1.TabIndex = 3;
            guna2Button1.Text = "Enter";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // MinimizeButton
            // 
            MinimizeButton.BackColor = Color.Silver;
            MinimizeButton.CheckedState.ImageSize = new Size(64, 64);
            MinimizeButton.Cursor = Cursors.Hand;
            MinimizeButton.HoverState.ImageSize = new Size(20, 30);
            MinimizeButton.Image = (Image)resources.GetObject("MinimizeButton.Image");
            MinimizeButton.ImageOffset = new Point(0, 0);
            MinimizeButton.ImageRotate = 0F;
            MinimizeButton.ImageSize = new Size(20, 30);
            MinimizeButton.Location = new Point(626, 1);
            MinimizeButton.Margin = new Padding(3, 2, 3, 2);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.PressedState.ImageSize = new Size(20, 30);
            MinimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges1;
            MinimizeButton.Size = new Size(43, 26);
            MinimizeButton.TabIndex = 5;
            MinimizeButton.Click += MinimizeButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Cursor = Cursors.Hand;
            ExitButton.CustomizableEdges = customizableEdges2;
            ExitButton.DisabledState.BorderColor = Color.DarkGray;
            ExitButton.DisabledState.CustomBorderColor = Color.DarkGray;
            ExitButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ExitButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ExitButton.FillColor = Color.Silver;
            ExitButton.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            ExitButton.ForeColor = Color.White;
            ExitButton.Location = new Point(669, 1);
            ExitButton.Margin = new Padding(3, 2, 3, 2);
            ExitButton.Name = "ExitButton";
            ExitButton.ShadowDecoration.CustomizableEdges = customizableEdges3;
            ExitButton.Size = new Size(43, 26);
            ExitButton.TabIndex = 4;
            ExitButton.Text = "X";
            ExitButton.Click += ExitButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 76, 123);
            ClientSize = new Size(712, 450);
            Controls.Add(MinimizeButton);
            Controls.Add(ExitButton);
            Controls.Add(guna2Button1);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(guna2HtmlLabel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Label label1;
        private Panel panel1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2ImageButton MinimizeButton;
        private Guna.UI2.WinForms.Guna2Button ExitButton;
    }
}
