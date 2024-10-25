using Grace.Event.ContextMenu;
using Grace.Event.Monster;
using Grace.Model;
using static Grace.Common.Util;
using Timer = System.Windows.Forms.Timer;

namespace Grace;

public partial class MonsterView : Form
{
    public event EventHandler<LoadMonstersEventArgs>? LoadMonstersEventHandler;
    public event EventHandler<FilterMonstersEventArgs>? FilterMonstersEventHandler;
    public event EventHandler? SelectMonsterEventHandler;
    public event EventHandler<TreeViewEventArgs>? SelectDropEventHandler;
    public event EventHandler? SaveDropEventHandler;

    // DropInfo ContextMenu
    public event EventHandler<InfoContextMenuEventArgs>? InfoRemoveItemEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? InfoSetItemEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? InfoSetDropGroupEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? InfoRenameDropGroupEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? InfoViewReferencesEventHandler;

    // TreeView ContextMenu
    public event EventHandler<TreeContextMenuEventArgs>? TreeRenameDropGroupEventHandler;
    public event EventHandler<TreeContextMenuEventArgs>? TreeViewReferencesEventHandler;
    public event EventHandler? AddSubIdEventHandler;
    public event EventHandler<TreeContextMenuEventArgs>? DeleteSubIdEventHandler;

    public Monster? SelectedMonster => monsterDataGrid.CurrentRow?.DataBoundItem as Monster;
    public DataGridView MonsterDataGrid => monsterDataGrid;
    public TreeView DropTreeView { get { return dropTreeView; } set { dropTreeView = value; } }
    private TextBox[] DropNames => GetControlsByName<TextBox>(groupBox_DropGroupDetails, "DropName");
    private NumericUpDown[] DropMinCounts => GetControlsByName<NumericUpDown>(groupBox_DropGroupDetails, "DropMinCount");
    private NumericUpDown[] DropMaxCounts => GetControlsByName<NumericUpDown>(groupBox_DropGroupDetails, "DropMaxCount");
    private MaskedTextBox[] DropChances => GetControlsByName<MaskedTextBox>(groupBox_DropGroupDetails, "DropChance");
    public ProgressBar ProgressBar => progressBar1;
    public bool HideMonstersWithoutTable => checkBox_NoTable.Checked;

    private Drop _currentDrop = new();
    public Drop CurrentDrop
    {
        get
        {
            Drop currentDrop = new()
            {
                Id = Convert.ToInt32(textBox_DropId.Text),
                SubId = Convert.ToInt32(textBox_DropId.Tag)
            };

            for (int i = 0; i < 10; i++)
            {
                currentDrop.DropItemIds[i] = _currentDrop.DropItemIds[i];
                currentDrop.ItemNames[i] = DropNames[i].Text;
                currentDrop.DropMinCounts[i] = Convert.ToInt32(DropMinCounts[i].Value);
                currentDrop.DropMaxCounts[i] = Convert.ToInt32(DropMaxCounts[i].Value);
                currentDrop.DropPercentages[i] = Convert.ToDouble(DropChances[i].Text);
            }

            _currentDrop = currentDrop;
            return _currentDrop;
        }
        set
        {
            double sum = 0.0;
            for (int i = 0; i < 10; i++)
            {
                DropNames[i].Text = value.ItemNames[i];
                DropMinCounts[i].Value = value.DropMinCounts[i];
                DropMaxCounts[i].Value = value.DropMaxCounts[i];
                DropChances[i].Text = value.DropPercentages[i].ToString("0.00000000");
                sum += value.DropPercentages[i];
            }

            string sumString = (sum).ToString("0.00");
            label_TotalPercentage.Text = $"Total: {sumString}";

            textBox_DropId.Text = value.Id.ToString();
            textBox_DropId.Tag = value.SubId;

            _currentDrop = value;
        }
    }

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
        btn_Save.Click += (sender, e) => { SaveDropEventHandler?.Invoke(sender, e); };

        // enable/disable DropInfo ContextMenuItems
        foreach (var textBox in DropNames)
            textBox.MouseDown += ShowDropInfoContextMenu;


        // DropInfo ContextMenu
        toolStripMenuItem_InfoRemove.Click += (sender, e) => InfoRemoveItemEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
        toolStripMenuItem_InfoSetItem.Click += (sender, e) => InfoSetItemEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
        toolStripMenuItem_InfoSetDropGroup.Click += (sender, e) => InfoSetDropGroupEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
        toolStripMenuItem_InfoRename.Click += (sender, e) => InfoRenameDropGroupEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
        toolStripMenuItem_InfoViewReferences.Click += (sender, e) => InfoViewReferencesEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));

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
        label_Result.ForeColor = success ? Color.Green : Color.Red;
        label_Result.Text = message;

        Timer timer = new();
        timer.Interval = 2000;
        timer.Start();
        timer.Tick += (sender, e) =>
        {
            Timer? _t = sender as Timer;
            label_Result.Text = "";
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

    private void ShowDropInfoContextMenu(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right || _currentDrop.Id == 0)
            return;

        if (sender is not TextBox textBox)
            return;

        int itemIndex = Array.IndexOf(DropNames, textBox);

        if (textBox.ContextMenuStrip == null || itemIndex == -1)
            return;

        int dropId = _currentDrop.DropItemIds[itemIndex];

        if (dropId == 0)
        {
            textBox.ContextMenuStrip.Items[0].Enabled = false;
            textBox.ContextMenuStrip.Items[1].Enabled = true;
            textBox.ContextMenuStrip.Items[2].Enabled = true;
            textBox.ContextMenuStrip.Items[3].Enabled = false;
            textBox.ContextMenuStrip.Items[4].Enabled = false;
        }
        else if (dropId > 0)
        {
            textBox.ContextMenuStrip.Items[0].Enabled = true;
            textBox.ContextMenuStrip.Items[1].Enabled = true;
            textBox.ContextMenuStrip.Items[2].Enabled = true;
            textBox.ContextMenuStrip.Items[3].Enabled = false;
            textBox.ContextMenuStrip.Items[4].Enabled = true;
        }
        else if (dropId < 0)
        {
            textBox.ContextMenuStrip.Items[0].Enabled = true;
            textBox.ContextMenuStrip.Items[1].Enabled = true;
            textBox.ContextMenuStrip.Items[2].Enabled = true;
            textBox.ContextMenuStrip.Items[3].Enabled = true;
            textBox.ContextMenuStrip.Items[4].Enabled = true;
        }
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

    private int GetItemIndex(object? sender)
    {
        if (sender is not ToolStripMenuItem menuItem)
            return -1;

        if (menuItem.GetCurrentParent() is not ContextMenuStrip contextMenu)
            return -1;

        if (contextMenu.SourceControl is not TextBox textBox)
            return -1;

        if (textBox is null)
            return -1;

        return Array.IndexOf(DropNames, textBox);
    }
}
