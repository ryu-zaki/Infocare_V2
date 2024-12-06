public class DataGridViewHelper
{
    private DataGridView _dataGridView;

    public DataGridViewHelper(DataGridView dataGridView)
    {
        _dataGridView = dataGridView;
        AttachEventHandlers();
    }

    private void AttachEventHandlers()
    {
        _dataGridView.CellBeginEdit += DataGridView_CellBeginEdit;
        _dataGridView.CellValueChanged += DataGridView_CellValueChanged;
        _dataGridView.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
    }

    // Make the event handlers public so they can be accessed outside
    public void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        if (e.ColumnIndex != 0) // Assuming checkbox is the first column
        {
            e.Cancel = true; // Prevent editing for other columns
        }
    }

    public void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex == 0) // Assuming the checkbox column is the first column
        {
            bool isChecked = (bool)_dataGridView.Rows[e.RowIndex].Cells[0].Value;

            // If the checkbox is checked, uncheck all others
            if (isChecked)
            {
                foreach (DataGridViewRow row in _dataGridView.Rows)
                {
                    if (row.Index != e.RowIndex) // Skip the current row
                    {
                        DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (checkBoxCell != null)
                        {
                            checkBoxCell.Value = false; // Uncheck the other checkboxes
                        }
                    }
                }
            }
        }
    }

    public void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (_dataGridView.CurrentCell is DataGridViewCheckBoxCell)
        {
            _dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}
