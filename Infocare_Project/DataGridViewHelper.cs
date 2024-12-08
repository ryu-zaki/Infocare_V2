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

    public void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        if (e.ColumnIndex != 0) 
        {
            e.Cancel = true;
        }
    }

    public void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex == 0)
        {
            bool isChecked = (bool)_dataGridView.Rows[e.RowIndex].Cells[0].Value;

            if (isChecked)
            {
                foreach (DataGridViewRow row in _dataGridView.Rows)
                {
                    if (row.Index != e.RowIndex) 
                    {
                        DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (checkBoxCell != null)
                        {
                            checkBoxCell.Value = false; 
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
