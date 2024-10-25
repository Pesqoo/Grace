using Grace.Model;

namespace Grace.View;
public partial class References : Form
{
    public References()
    {
        StartPosition = FormStartPosition.CenterParent;
        InitializeComponent();
    }

    public void SetDataSource(List<Monster> monsters)
    {
        monsterDataGrid.DataSource = monsters;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Escape)
        {
            this.Close();
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }
}
