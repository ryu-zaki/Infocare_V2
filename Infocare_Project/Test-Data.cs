using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infocare_Project_1
{
    public partial class Test_Data : Form
    {
        public Test_Data()
        {
            InitializeComponent();
        }

        private void DataGridViewList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0) // Replace with your checkbox column index if different
            {
                e.Cancel = true; // Prevent editing for other columns
            }
        }

        private void DataGridViewList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0) // Assuming the checkbox column is the first column
            {
                // Check if any checkbox is checked
                bool isAnyChecked = false;
                foreach (DataGridViewRow row in DataGridViewList.Rows)
                {
                    if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        isAnyChecked = true;
                        break;
                    }
                }

            }
        }

        private void DataGridViewList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DataGridViewList.CurrentCell is DataGridViewCheckBoxCell)
            {
                // Commit the edit when a checkbox is clicked
                DataGridViewList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
