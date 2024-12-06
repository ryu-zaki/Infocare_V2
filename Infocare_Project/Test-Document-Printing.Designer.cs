namespace Infocare_Project_1
{
    partial class Test_Document_Printing
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            DataGridViewList = new Guna.UI2.WinForms.Guna2DataGridView();
            checkboxcolumn = new DataGridViewCheckBoxColumn();
            ViewButton = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)DataGridViewList).BeginInit();
            SuspendLayout();
            // 
            // mySqlCommand1
            // 
            mySqlCommand1.CacheAge = 0;
            mySqlCommand1.Connection = null;
            mySqlCommand1.EnableCaching = false;
            mySqlCommand1.Transaction = null;
            // 
            // DataGridViewList
            // 
            DataGridViewList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(160, 160, 160);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            DataGridViewList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewList.BackgroundColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(13, 41, 80);
            dataGridViewCellStyle2.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(13, 41, 80);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewList.ColumnHeadersHeight = 39;
            DataGridViewList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            DataGridViewList.Columns.AddRange(new DataGridViewColumn[] { checkboxcolumn });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            DataGridViewList.DefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewList.GridColor = Color.FromArgb(179, 230, 251);
            DataGridViewList.Location = new Point(12, 84);
            DataGridViewList.Name = "DataGridViewList";
            DataGridViewList.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            DataGridViewList.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            DataGridViewList.RowHeadersVisible = false;
            DataGridViewList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            DataGridViewList.RowTemplate.Height = 35;
            DataGridViewList.Size = new Size(776, 391);
            DataGridViewList.TabIndex = 150;
            DataGridViewList.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightBlue;
            DataGridViewList.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(160, 160, 160);
            DataGridViewList.ThemeStyle.AlternatingRowsStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            DataGridViewList.ThemeStyle.AlternatingRowsStyle.ForeColor = SystemColors.ControlText;
            DataGridViewList.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            DataGridViewList.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Black;
            DataGridViewList.ThemeStyle.BackColor = Color.FromArgb(240, 240, 240);
            DataGridViewList.ThemeStyle.GridColor = Color.FromArgb(179, 230, 251);
            DataGridViewList.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(13, 41, 80);
            DataGridViewList.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            DataGridViewList.ThemeStyle.HeaderStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            DataGridViewList.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            DataGridViewList.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            DataGridViewList.ThemeStyle.HeaderStyle.Height = 39;
            DataGridViewList.ThemeStyle.ReadOnly = false;
            DataGridViewList.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(240, 240, 240);
            DataGridViewList.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridViewList.ThemeStyle.RowsStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            DataGridViewList.ThemeStyle.RowsStyle.ForeColor = Color.Black;
            DataGridViewList.ThemeStyle.RowsStyle.Height = 35;
            DataGridViewList.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            DataGridViewList.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
            DataGridViewList.CellContentClick += DataGridViewList_CellContentClick;
            // 
            // checkboxcolumn
            // 
            checkboxcolumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.NullValue = false;
            checkboxcolumn.DefaultCellStyle = dataGridViewCellStyle3;
            checkboxcolumn.HeaderText = "Select";
            checkboxcolumn.Name = "checkboxcolumn";
            checkboxcolumn.Resizable = DataGridViewTriState.False;
            // 
            // ViewButton
            // 
            ViewButton.BackColor = Color.DimGray;
            ViewButton.BorderRadius = 15;
            ViewButton.CustomizableEdges = customizableEdges1;
            ViewButton.DisabledState.BorderColor = Color.DarkGray;
            ViewButton.DisabledState.CustomBorderColor = Color.DarkGray;
            ViewButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ViewButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ViewButton.FillColor = SystemColors.ButtonFace;
            ViewButton.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            ViewButton.ForeColor = Color.Black;
            ViewButton.ImageAlign = HorizontalAlignment.Left;
            ViewButton.ImageOffset = new Point(2, 0);
            ViewButton.ImageSize = new Size(40, 40);
            ViewButton.Location = new Point(294, 34);
            ViewButton.Margin = new Padding(3, 2, 3, 2);
            ViewButton.Name = "ViewButton";
            ViewButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            ViewButton.Size = new Size(143, 33);
            ViewButton.TabIndex = 151;
            ViewButton.Text = "View Document";
            ViewButton.TextFormatNoPrefix = true;
            ViewButton.Visible = false;
            ViewButton.Click += ViewButton_Click;
            // 
            // Test_Document_Printing
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 515);
            Controls.Add(ViewButton);
            Controls.Add(DataGridViewList);
            Name = "Test_Document_Printing";
            Text = "Test_Document_Printing";
            Load += Test_Document_Printing_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridViewList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private Guna.UI2.WinForms.Guna2DataGridView DataGridViewList;
        private DataGridViewCheckBoxColumn checkboxcolumn;
        private Guna.UI2.WinForms.Guna2Button ViewButton;
    }
}