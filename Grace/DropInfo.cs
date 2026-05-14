using Grace.Cache;
using Grace.Event.ContextMenu;
using Grace.Model;
using static Grace.Common.Util;

namespace Grace;

public partial class DropInfo : UserControl
{
    public event EventHandler? SaveDropEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? RemoveItemEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? SetItemEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? SetDropGroupEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? RenameDropGroupEventHandler;
    public event EventHandler<InfoContextMenuEventArgs>? ViewReferencesEventHandler;

    private TextBox[] DropNames => GetControlsByName<TextBox>(groupBox_DropInfo, "DropName");
    private NumericUpDown[] DropMinCounts => GetControlsByName<NumericUpDown>(groupBox_DropInfo, "DropMinCount");
    private NumericUpDown[] DropMaxCounts => GetControlsByName<NumericUpDown>(groupBox_DropInfo, "DropMaxCount");
    private MaskedTextBox[] DropChances => GetControlsByName<MaskedTextBox>(groupBox_DropInfo, "DropChance");

    private Drop? _currentDrop;
    public Drop? CurrentDrop
    {
        get
        {
            _currentDrop = CreateDropFromUI();
            return _currentDrop;
        }
        set
        {
            UpdateUIFromDrop(value);
            _currentDrop = value;
        }
    }

    private void InitializeEvent()
    {
        btn_Save.Click += (sender, e) => { SaveDropEventHandler?.Invoke(sender, e); };

        foreach (var textBox in DropNames)
            textBox.MouseDown += ShowDropInfoContextMenu;

        toolStripMenuItem_InfoRemove.Click += (sender, e) => RemoveItemEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
        toolStripMenuItem_InfoSetItem.Click += (sender, e) => SetItemEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
        toolStripMenuItem_InfoSetDropGroup.Click += (sender, e) => SetDropGroupEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
        toolStripMenuItem_InfoRename.Click += (sender, e) => RenameDropGroupEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
        toolStripMenuItem_InfoViewReferences.Click += (sender, e) => ViewReferencesEventHandler?.Invoke(sender, new InfoContextMenuEventArgs(GetItemIndex(sender)));
    }

    public DropInfo()
    {
        InitializeComponent();
        InitializeEvent();
    }

    private Drop CreateDropFromUI()
    {
        var drop = new Drop
        {
            Id = Convert.ToInt32(textBox_DropId.Text),
            SubId = Convert.ToInt32(textBox_DropId.Tag)
        };

        for (int i = 0; i < 10; i++)
        {
            drop.DropItemIds[i] = _currentDrop?.DropItemIds[i] ?? 0;
            drop.ItemNames[i] = DropNames[i].Text;
            drop.DropMinCounts[i] = Convert.ToInt32(DropMinCounts[i].Value);
            drop.DropMaxCounts[i] = Convert.ToInt32(DropMaxCounts[i].Value);
            drop.DropPercentages[i] = Convert.ToDouble(DropChances[i].Text);
        }

        return drop;
    }

    private void UpdateUIFromDrop(Drop? drop)
    {
        double sum = 0.0;

        for (int i = 0; i < 10; i++)
        {
            var dropId = drop?.DropItemIds[i] ?? 0;
            if (dropId == 0)
            {
                DropNames[i].Text = drop?.ItemNames[i] ?? "";
            }
            else if (dropId > 0)
            {
                DropNames[i].Text = ItemCache.Cache[dropId];
            }
            else if (dropId < 0)
            {
                DropGroupCache.Cache.TryGetValue(dropId, out Drop? dropGroup);
                var alias = dropGroup?.Alias;

                DropNames[i].Text = $"{dropId}" + (alias != null && alias != string.Empty ? $": {alias}" : string.Empty);
            }

            DropNames[i].Text = drop?.ItemNames[i] ?? "";
            DropMinCounts[i].Value = drop?.DropMinCounts[i] ?? 0;
            DropMaxCounts[i].Value = drop?.DropMaxCounts[i] ?? 0;
            DropChances[i].Text = drop?.DropPercentages[i].ToString("0.00000000") ?? "0.00000000";
            sum += drop?.DropPercentages[i] ?? 0;
        }

        label_TotalPercentage.Text = $"Total: {sum:0.00}";

        textBox_DropId.Text = drop?.Id.ToString() ?? "";
        textBox_DropId.Tag = drop?.SubId;
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

    private void ShowDropInfoContextMenu(object? sender, MouseEventArgs e)
    {
        if (_currentDrop == null || _currentDrop.Id == 0 || e.Button != MouseButtons.Right)
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
}
