using Grace.Model;

namespace Grace.View;
public partial class References : Form
{
    public References()
    {
        StartPosition = FormStartPosition.CenterParent;
        InitializeComponent();
    }

    public void SetMonsterDataSource(List<Monster> monsters)
    {
        monsterDataGrid.DataSource = monsters;
    }

    public void SetItemDataSource(List<Item> items)
    {
        itemDataGrid.DataSource = items;
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

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        itemDataGrid.DataSource = null;
        monsterDataGrid.DataSource = null;
    }
}
