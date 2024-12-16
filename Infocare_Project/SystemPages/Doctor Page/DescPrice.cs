using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminDoctor_Panel.SystemPages.Doctor_Page
{

    public partial class DescPrice : UserControl
    {
        public Action<DescPrice> RemoveTile;
        public DescPrice()
        {
            InitializeComponent();
        }

        public string Desc
        {
            get { return descTextbox.Text; }
        }

        public decimal Price
        {
            get { return decimal.Parse(priceTextbox.Text); }
        }

        private void DescPrice_Load(object sender, EventArgs e)
        {


        }

        private void guna2TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            RemoveTile.Invoke(this);
        }
    }
}
