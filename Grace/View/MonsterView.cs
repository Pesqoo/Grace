using Grace.Event.ContextMenu;
using Grace.Event.Monster;
using Grace.Model;
using Timer = System.Windows.Forms.Timer;

namespace Grace;

public partial class MonsterView : Form
{
    public event EventHandler<LoadMonstersEventArgs>? LoadMonstersEventHandler;
    public event EventHandler<FilterMonstersEventArgs>? FilterMonstersEventHandler;
    public event EventHandler? SelectMonsterEventHandler;
    public event EventHandler<TreeViewEventArgs>? SelectDropEventHandler;

    // TreeView ContextMenu
    public event EventHandler<TreeContextMenuEventArgs>? TreeRenameDropGroupEventHandler;
    public event EventHandler<TreeContextMenuEventArgs>? TreeViewReferencesEventHandler;
    public event EventHandler? AddSubIdEventHandler;
    public event EventHandler<TreeContextMenuEventArgs>? DeleteSubIdEventHandler;

    public Monster? SelectedMonster => monsterDataGrid.CurrentRow?.DataBoundItem as Monster;
    public DataGridView MonsterDataGrid => monsterDataGrid;
    public TreeView DropTreeView { get { return dropTreeView; } set { dropTreeView = value; } }
    public ProgressBar ProgressBar => progressBar1;
    public bool HideMonstersWithoutTable => checkBox_NoTable.Checked;

    public MonsterView()
    {
        InitializeComponent();
        InitializeEvent();

        monsterDataGrid.AutoGenerateColumns = false;
    }

    private void InitializeEvent()
    {
        btn_ReloadData.Click += (sender, e) => { LoadMonstersEventHandler?.Invoke(sender, new LoadMonstersEventArgs(true)); };
        btn_Filter.Click += (sender, e) => { FilterMonstersEventHandler?.Invoke(sender, new FilterMonstersEventArgs(GetFilterType())); };
        // monsterDataGrid.CellClick += (sender, e) => { SelectMonsterEventHandler?.Invoke(sender, e); };
        monsterDataGrid.CellClick += (sender, e) => { SelectMonsterEventHandler?.Invoke(sender, e); };
        dropTreeView.AfterSelect += (sender, e) => { SelectDropEventHandler?.Invoke(sender, e); };


        // TreeView ContextMenu
        toolStripMenuItem_TreeViewRename.Click += (sender, e) => TreeRenameDropGroupEventHandler?.Invoke(sender, new TreeContextMenuEventArgs(dropTreeView.SelectedNode));
        toolStripMenuItem_TreeViewViewReferences.Click += (sender, e) => TreeViewReferencesEventHandler?.Invoke(sender, new TreeContextMenuEventArgs(dropTreeView.SelectedNode));
        toolStripMenuItem_AddSubId.Click += (sender, e) => AddSubIdEventHandler?.Invoke(sender, e);
        toolStripMenuItem_DeleteSubId.Click += (sender, e) => DeleteSubIdEventHandler?.Invoke(sender, new TreeContextMenuEventArgs(dropTreeView.SelectedNode));

        // set SelectedNode on right click
        dropTreeView.NodeMouseClick += SetSelectedNode;
        contextMenuStrip_TreeViewItem.Opening += HandleTreeNodeContextMenu;
    }

    public void AttachToParent(Control parent)
    {
        this.Size = parent.Size;
        this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        this.TopLevel = false;
        this.Show();
        parent.Controls.Add(this);
    }

    public void ShowResult(string message, bool success = true)
    {
        DropInfo.label_Result.ForeColor = success ? Color.Green : Color.Red;
        DropInfo.label_Result.Text = message;

        Timer timer = new();
        timer.Interval = 2000;
        timer.Start();
        timer.Tick += (sender, e) =>
        {
            Timer? _t = sender as Timer;
            DropInfo.label_Result.Text = "";
            _t?.Stop();
        };
    }

    private MonsterFilterType GetFilterType()
    {
        int checkedIndex = -1;
        foreach (RadioButton radioButton in groupBox_Filter.Controls.OfType<RadioButton>())
        {
            if (radioButton.Checked)
            {
                checkedIndex = groupBox_Filter.Controls.IndexOf(radioButton);
                break;
            }
        }

        return (MonsterFilterType)checkedIndex;
    }

    private void SetSelectedNode(object? sender, TreeNodeMouseClickEventArgs e)
    {
        dropTreeView.SelectedNode = e.Node;
    }

    private void HandleTreeNodeContextMenu(object? sender, EventArgs e)
    {
        TreeNode selectedNode = dropTreeView.SelectedNode;

        if (selectedNode == null)
            return;

        if (!int.TryParse(selectedNode.Name, out int dropId))
            return;

        if (dropId == 0)
        {
            toolStripMenuItem_TreeViewRename.Enabled = false;
            toolStripMenuItem_TreeViewViewReferences.Enabled = false;
        }
        else if (dropId > 0)
        {
            toolStripMenuItem_TreeViewRename.Enabled = false;
            toolStripMenuItem_TreeViewViewReferences.Enabled = true;
        }
        else if (dropId < 0)
        {
            toolStripMenuItem_TreeViewRename.Enabled = true;
            toolStripMenuItem_TreeViewViewReferences.Enabled = true;
        }
    }
}
