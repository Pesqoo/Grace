using Grace.Cache;
using Grace.Event;
using Grace.Model;
using Grace.Model.Repository;
using Grace.View;
using static Grace.Common.Util;

namespace Grace.Presenter;

public class MonsterPresenter
{
    private readonly MonsterView _monsterView;
    private readonly UpdateWarningView _updateWarningView;

    private readonly FilterMonsterPresenter _filterMonsterPresenter;
    private readonly SetItemPresenter _setItemPresenter;
    private readonly SetDropGroupPresenter _setDropGroupPresenter;
    private readonly RenameDropGroupPresenter _renameDropGroupPresenter;

    private readonly DropRepository _dropRepository;
    private readonly MonsterRepository _monsterRepository;

    private readonly MonsterCache _monsterCache;
    private readonly ItemCache _itemCache;
    private readonly DropGroupCache _dropGroupCache;

    private List<Monster> _monsters = [];
    private Monster? _selectedMonster;

    public MonsterPresenter(
        MonsterView monsterView,
        UpdateWarningView updateWarningView,
        FilterMonsterPresenter filterMonsterPresenter,
        SetItemPresenter setItemPresenter,
        SetDropGroupPresenter setDropGroupPresenter,
        RenameDropGroupPresenter renameDropGroupPresenter,
        DropRepository dropRepository,
        MonsterRepository monsterRepository,
        MonsterCache monsterCache,
        ItemCache itemCache,
        DropGroupCache dropGroupCache)
    {
        _monsterView = monsterView;
        _updateWarningView = updateWarningView;

        _filterMonsterPresenter = filterMonsterPresenter;
        _setItemPresenter = setItemPresenter;
        _setDropGroupPresenter = setDropGroupPresenter;
        _renameDropGroupPresenter = renameDropGroupPresenter;

        _dropRepository = dropRepository;
        _monsterRepository = monsterRepository;

        _monsterCache = monsterCache;
        _itemCache = itemCache;
        _dropGroupCache = dropGroupCache;

        _monsterView.Load += async (sender, e) => await MonsterView_LoadMonsters(sender, new LoadMonstersEventArgs(false));
        _monsterView.LoadMonstersEventHandler += async (sender, e) => await MonsterView_LoadMonsters(sender, e);
        _monsterView.FilterMonstersEventHandler += async (sender, e) => await MonsterView_FilterMonsters(sender, e);
        _monsterView.SelectMonsterEventHandler += async (sender, e) => await MonsterView_SelectMonster(sender, e);
        _monsterView.SelectDropEventHandler += async (sender, e) => await MonsterView_SelectDrop(sender, e);
        _monsterView.SaveDropEventHandler += async (sender, e) => await MonsterView_UpdateDrop(sender, e);

        // context menu
        _monsterView.RemoveItemEventHandler += MonsterView_RemoveDrop;
        _monsterView.SetItemEventHandler += MonsterView_SetDropItem;
        _monsterView.SetDropGroupEventHandler += MonsterView_SetDropGroup;
        _monsterView.RenameDropGroupEventHandler += MonsterView_RenameDropGroup;
    }

    public async Task MonsterView_LoadMonsters(object? sender, LoadMonstersEventArgs e)
    {
        if (e.ReloadData)
        {
            await _monsterCache.Init();
            await _itemCache.Init();
            await _dropGroupCache.Init();
        }

        var result = MonsterCache.Cache;
        if (_monsterView.HideMonstersWithoutTable)
            result = result.Where(v => v.DropTableId != 0).ToList();

        _monsters = result;
        _monsterView.MonsterDataGrid.DataSource = _monsters;

        if (_monsters.Count > 0)
            await MonsterView_SelectMonster(this, new DataGridViewCellEventArgs(0, 0));
    }

    private async Task MonsterView_FilterMonsters(object? sender, FilterMonstersEventArgs e)
    {
        if (e.FilterType == MonsterFilterType.ALL)
        {
            var result = MonsterCache.Cache;
            if (_monsterView.HideMonstersWithoutTable)
                result = result.Where(v => v.DropTableId != 0).ToList();
            _monsters = result;
        }
        else
        {
            List<Monster>? result = await _filterMonsterPresenter.OnShowView(e.FilterType);
            if (result != null)
            {
                _monsters = result;
            }
        }

        _monsterView.MonsterDataGrid.DataSource = _monsters;
        if (_monsters.Count > 0)
            await MonsterView_SelectMonster(this, new DataGridViewCellEventArgs(0, 0));
        else
        {
            _monsterView.DropTreeView.Nodes.Clear();
            _monsterView.CurrentDrop = new();
        }
    }

    private async Task MonsterView_SelectMonster(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1)
            return;

        _selectedMonster = _monsters[e.RowIndex];
        await UpdateDropTreeView();
    }

    private async Task UpdateDropTreeView()
    {
        _monsterView.MonsterDataGrid.Enabled = false;
        _monsterView.ProgressBar.Visible = true;

        List<Drop> dropTables = await _dropRepository.GetTableById(_selectedMonster!.DropTableId);

        _monsterView.DropTreeView.Nodes.Clear();
        _monsterView.DropTreeView.BeginUpdate();

        if (dropTables.Count == 0)
        {
            TreeNode noDataNode = new("No drop data available for this Droplink ID.");
            _monsterView.DropTreeView.Nodes.Add(noDataNode);
            _monsterView.DropTreeView.EndUpdate();
            _monsterView.MonsterDataGrid.Enabled = true;
            _monsterView.ProgressBar.Visible = false;
            return;
        }

        foreach (Drop dropTable in dropTables)
        {
            TreeNode subIdNode = new($"Sub ID #{dropTable.SubId}")
            {
                Tag = $"{dropTable.Id},{dropTable.SubId}"
            };

            for (int i = 0; i < 10; i++)
            {
                int dropId = dropTable.DropItemIds[i];

                if (dropId > 0)
                {
                    var itemNode = CreateItemNode(dropId);
                    itemNode.ContextMenuStrip = _monsterView.contextMenuStrip_EditDrop;
                    subIdNode.Nodes.Add(itemNode);
                }

                else if (dropId < 0)
                {
                    var groupNode = await CreateDropGroupNode(_dropRepository, dropId, dropTable.ItemNames[i]);
                    groupNode.ContextMenuStrip = _monsterView.contextMenuStrip_EditDrop;
                    subIdNode.Nodes.Add(groupNode);
                }

                else
                {
                    var emptyNode = new TreeNode("Empty slot");
                    emptyNode.Name = dropId.ToString();
                    emptyNode.ContextMenuStrip = _monsterView.contextMenuStrip_EditDrop;
                    subIdNode.Nodes.Add(emptyNode);
                }
            }

            _monsterView.DropTreeView.Nodes.Add(subIdNode);
        }

        _monsterView.DropTreeView.EndUpdate();
        _monsterView.ProgressBar.Visible = false;
        _monsterView.MonsterDataGrid.Enabled = true;
    }



    private async Task MonsterView_SelectDrop(object? sender, TreeViewEventArgs e)
    {
        // load dropGroup
        if (e.Node?.Tag is int dropId)
        {
            Drop? dropGroup = await _dropRepository.GetGroupById(dropId);

            if (dropGroup == null)
            {
                MessageBox.Show("No drop information found for the selected monster.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _monsterView.CurrentDrop = dropGroup;
        }
        else if (e.Node?.Tag is string tagString)
        {
            int[] ids = tagString.Split(',').Select(int.Parse).ToArray();
            Drop? dropTable = await _dropRepository.GetTableByIdAndSubId(ids[0], ids[1]);

            if (dropTable == null)
            {
                MessageBox.Show("No drop information found for the selected monster.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _monsterView.CurrentDrop = dropTable;
        }
    }

    private async Task MonsterView_UpdateDrop(object? sender, EventArgs e)
    {
        Drop drop = _monsterView.CurrentDrop;
        int result;

        var affectedMonsters = await _monsterRepository.GetByReferenceToDropGroupId(drop.Id);

        if (affectedMonsters.Count > 1)
        {
            _updateWarningView.SetDataSource(affectedMonsters);
            DialogResult dialogResult = _updateWarningView.ShowDialog();

            if (dialogResult != DialogResult.OK)
                return;
        }

        if (drop.SubId == -1)
        {
            result = await _dropRepository.UpdateGroup(drop);
            DropGroupCache.Cache[drop.Id] = drop;
        }
        else
            result = await _dropRepository.UpdateTable(drop);

        await UpdateDropTreeView();
        _monsterView.ShowResult($"{result} row(s) have been updated.");
    }

    private void MonsterView_RemoveDrop(object? sender, ContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        Drop tempDrop = _monsterView.CurrentDrop;
        tempDrop.DropItemIds[e.ItemIndex] = 0;
        tempDrop.DropMaxCounts[e.ItemIndex] = 0;
        tempDrop.DropMinCounts[e.ItemIndex] = 0;
        tempDrop.DropPercentages[e.ItemIndex] = 0;
        tempDrop.ItemNames[e.ItemIndex] = "Empty slot";
        _monsterView.CurrentDrop = tempDrop;
    }

    private void MonsterView_SetDropItem(object? sender, ContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        var itemId = _setItemPresenter.OnShowView();

        if (itemId == 0)
            return;

        Drop tempDrop = _monsterView.CurrentDrop;
        tempDrop.DropItemIds[e.ItemIndex] = itemId;
        tempDrop.ItemNames[e.ItemIndex] = ItemCache.Cache[itemId];
        _monsterView.CurrentDrop = tempDrop;
    }

    private void MonsterView_SetDropGroup(object? sender, ContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        var dropGroupId = _setDropGroupPresenter.OnShowView();

        if (dropGroupId == 0)
            return;

        Drop tempDrop = _monsterView.CurrentDrop;
        var dropGroupName = DropGroupCache.Cache[dropGroupId].Alias;
        tempDrop.DropItemIds[e.ItemIndex] = dropGroupId;
        tempDrop.ItemNames[e.ItemIndex] = $"{dropGroupId}" + (dropGroupName != null && dropGroupName != string.Empty ? $": {dropGroupName}" : string.Empty);
        _monsterView.CurrentDrop = tempDrop;
    }

    private void MonsterView_RenameDropGroup(object? sender, ContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        var newName = _renameDropGroupPresenter.OnShowView();

        if (newName == null)
        {
            return;
        }

        int dropGroupId = _monsterView.CurrentDrop.DropItemIds[e.ItemIndex];
        DropGroupCache.AddDropGroupName(dropGroupId, newName);

        var nodes = _monsterView.DropTreeView.SelectedNode.Nodes.FindByName(dropGroupId.ToString());

        foreach (var node in nodes)
        {
            node.Text = $"{dropGroupId}: {newName}";
        }

        Drop tempDrop = _monsterView.CurrentDrop;
        tempDrop.ItemNames[e.ItemIndex] = $"{dropGroupId}: {newName}";
        _monsterView.CurrentDrop = tempDrop;
    }
}
