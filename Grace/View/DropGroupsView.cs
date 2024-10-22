using Grace.Event;
using Grace.Event.DropGroup;
using Grace.Model;
using static Grace.Common.Util;
using Timer = System.Windows.Forms.Timer;


namespace Grace.View;

public partial class DropGroupsView : Form
{
    public event EventHandler<LoadDropGroupsEventArgs>? LoadDropGroupsEventHandler;
    public event EventHandler<FilterDropGroupsEventArgs>? FilterDropGroupsEventHandler;
    public event EventHandler<DataGridViewCellEventArgs>? SelectDropGroupEventHandler;
    public event EventHandler<TreeViewEventArgs>? SelectDropEventHandler;
    public event EventHandler? SaveDropEventHandler;
    public event EventHandler<ContextMenuEventArgs>? RemoveItemEventHandler;
    public event EventHandler<ContextMenuEventArgs>? SetItemEventHandler;
    public event EventHandler<ContextMenuEventArgs>? SetDropGroupEventHandler;
    public event EventHandler<ContextMenuEventArgs>? RenameDropGroupEventHandler;

    public DataGridView DataGridView { get { return dropGroupDataGrid; } }
    public TreeView DropTreeView { get { return dropTreeView; } }
    private TextBox[] DropNames => GetControlsByName<TextBox>(groupBox_DropGroupInfo, "DropName");
    private NumericUpDown[] DropMinCounts => GetControlsByName<NumericUpDown>(groupBox_DropGroupInfo, "DropMinCount");
    private NumericUpDown[] DropMaxCounts => GetControlsByName<NumericUpDown>(groupBox_DropGroupInfo, "DropMaxCount");
    private MaskedTextBox[] DropChances => GetControlsByName<MaskedTextBox>(groupBox_DropGroupInfo, "DropChance");
    public ProgressBar ProgressBar => progressBar;

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

    public DropGroupsView()
    {
        InitializeComponent();
        InitializeEvent();

        dropGroupDataGrid.AutoGenerateColumns = false;

    }

    private void InitializeEvent()
    {
        btn_ReloadData.Click += (sender, e) => { LoadDropGroupsEventHandler?.Invoke(sender, new LoadDropGroupsEventArgs(true)); };
        btn_Filter.Click += (sender, e) => { FilterDropGroupsEventHandler?.Invoke(sender, new FilterDropGroupsEventArgs(GetFilterType())); };
        dropGroupDataGrid.CellClick += (sender, e) => { SelectDropGroupEventHandler?.Invoke(sender, e); };
        dropTreeView.AfterSelect += (sender, e) => { SelectDropEventHandler?.Invoke(sender, e); };
        btn_Save.Click += (sender, e) => { SaveDropEventHandler?.Invoke(sender, e); };

        foreach (var textBox in DropNames)
            textBox.MouseDown += ShowDropInfoContextMenu;

        toolStripMenuItem_Remove.Click += (sender, e) => RemoveItemEventHandler?.Invoke(sender, new ContextMenuEventArgs(ContextMenuEventType.REMOVE, GetItemIndex(sender)));
        toolStripMenuItem_SetItem.Click += (sender, e) => SetItemEventHandler?.Invoke(sender, new ContextMenuEventArgs(ContextMenuEventType.SET_ITEM, GetItemIndex(sender)));
        toolStripMenuItem_SetDropGroup.Click += (sender, e) => SetDropGroupEventHandler?.Invoke(sender, new ContextMenuEventArgs(ContextMenuEventType.SET_DROPGROUP, GetItemIndex(sender)));
        toolStripMenuItem_Rename.Click += (sender, e) => RenameDropGroupEventHandler?.Invoke(sender, new ContextMenuEventArgs(ContextMenuEventType.RENAME, GetItemIndex(sender)));
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

    public void SetDropGroupDataSource(List<Drop> drops)
    {
        dropGroupDataGrid.DataSource = drops;
    }

    private DropGroupFilterType GetFilterType()
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

        return (DropGroupFilterType)checkedIndex;
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
        }
        else if (dropId > 0)
        {
            textBox.ContextMenuStrip.Items[0].Enabled = true;
            textBox.ContextMenuStrip.Items[1].Enabled = true;
            textBox.ContextMenuStrip.Items[2].Enabled = true;
            textBox.ContextMenuStrip.Items[3].Enabled = false;
        }
        else if (dropId < 0)
        {
            textBox.ContextMenuStrip.Items[0].Enabled = true;
            textBox.ContextMenuStrip.Items[1].Enabled = true;
            textBox.ContextMenuStrip.Items[2].Enabled = true;
            textBox.ContextMenuStrip.Items[3].Enabled = true;
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
