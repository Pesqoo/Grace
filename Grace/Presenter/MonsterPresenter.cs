using Grace.Cache;
using Grace.Event.ContextMenu;
using Grace.Event.Monster;
using Grace.Model;
using Grace.Model.Repository;
using Grace.View;
using static Grace.Common.Util;

namespace Grace.Presenter;

public class MonsterPresenter
{
    private readonly MonsterView _monsterView;
    private readonly UpdateWarningView _updateWarningView;
    private readonly References _referencesView;

    private readonly FilterMonsterPresenter _filterMonsterPresenter;
    private readonly SetItemPresenter _setItemPresenter;
    private readonly SetDropGroupPresenter _setDropGroupPresenter;
    private readonly RenameDropGroupPresenter _renameDropGroupPresenter;

    private readonly DropRepository _dropRepository;
    private readonly MonsterRepository _monsterRepository;

    private readonly MonsterCache _monsterCache;
    private readonly ItemCache _itemCache;
    private readonly DropGroupCache _dropGroupCache;

    public MonsterPresenter(
        MonsterView monsterView,
        UpdateWarningView updateWarningView,
        References referencesView,
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
        _referencesView = referencesView;

        _filterMonsterPresenter = filterMonsterPresenter;
        _setItemPresenter = setItemPresenter;
        _setDropGroupPresenter = setDropGroupPresenter;
        _renameDropGroupPresenter = renameDropGroupPresenter;

        _dropRepository = dropRepository;
        _monsterRepository = monsterRepository;

        _monsterCache = monsterCache;
        _itemCache = itemCache;
        _dropGroupCache = dropGroupCache;

        _monsterView.Load += async (sender, e) => await MonsterView_LoadMonsters(sender, new LoadMonstersEventArgs(reloadCache: false));
        _monsterView.LoadMonstersEventHandler += async (sender, e) => await MonsterView_LoadMonsters(sender, e);
        _monsterView.SelectMonsterEventHandler += async (sender, e) => await UpdateDropTreeView();
        _monsterView.FilterMonstersEventHandler += async (sender, e) => await MonsterView_FilterMonsters(sender, e);
        _monsterView.SelectDropEventHandler += async (sender, e) => await MonsterView_SelectDrop(sender, e);
        _monsterView.DropInfo.SaveDropEventHandler += async (sender, e) => await MonsterView_UpdateDrop(sender, e);

        // DropInfo ContextMenu
        _monsterView.DropInfo.RemoveItemEventHandler += MonsterView_InfoRemoveDrop;
        _monsterView.DropInfo.SetItemEventHandler += MonsterView_InfoSetDropItem;
        _monsterView.DropInfo.SetDropGroupEventHandler += MonsterView_InfoSetDropGroup;
        _monsterView.DropInfo.RenameDropGroupEventHandler += MonsterView_InfoRenameDropGroup;
        _monsterView.DropInfo.ViewReferencesEventHandler += async (sender, e) => await MonsterView_InfoViewReferences(sender, e);

        // TreeView ContextMenu
        _monsterView.TreeRenameDropGroupEventHandler += MonsterView_TreeRenameDropGroup;
        _monsterView.TreeViewReferencesEventHandler += async (sender, e) => await MonsterView_TreeViewReferences(sender, e);

        _monsterView.AddSubIdEventHandler += async (sender, e) => await MonsterView_AddSubId(sender, e);
        _monsterView.DeleteSubIdEventHandler += async (sender, e) => await MonsterView_DeleteSubId(sender, e);
    }

    public async Task MonsterView_LoadMonsters(object? sender, LoadMonstersEventArgs e)
    {
        if (e.ReloadCache)
        {
            await _monsterCache.Init();
            await _itemCache.Init();
            await _dropGroupCache.Init();
        }

        var monsters = MonsterCache.Cache;
        if (_monsterView.HideMonstersWithoutTable)
            monsters = monsters.Where(v => v.DropTableId != 0).ToList();

        _monsterView.MonsterDataGrid.DataSource = monsters;
        await UpdateDropTreeView();
    }

    private async Task MonsterView_FilterMonsters(object? sender, FilterMonstersEventArgs e)
    {
        List<Monster>? filterResult;
        if (e.FilterType == MonsterFilterType.ALL)
        {
            filterResult = MonsterCache.Cache;
        }
        else
        {
            filterResult = await _filterMonsterPresenter.OnShowView(e.FilterType);
            if (filterResult == null)
                return;
        }

        if (_monsterView.HideMonstersWithoutTable)
            filterResult = filterResult.Where(v => v.DropTableId != 0).ToList();

        _monsterView.MonsterDataGrid.DataSource = filterResult;
        await UpdateDropTreeView();
    }

    private async Task UpdateDropTreeView()
    {
        if (_monsterView.SelectedMonster is not Monster selectedMonster)
        {
            _monsterView.DropTreeView.Nodes.Clear();
            _monsterView.DropInfo.CurrentDrop = null;
            return;
        }

        _monsterView.MonsterDataGrid.Enabled = false;
        _monsterView.ProgressBar.Visible = true;

        List<Drop> dropTables = await _dropRepository.GetTableById(selectedMonster.DropTableId);

        _monsterView.DropTreeView.Nodes.Clear();
        _monsterView.DropTreeView.BeginUpdate();

        if (dropTables.Count == 0)
        {
            TreeNode noDataNode = new($"No data available for this DropTable ID ({selectedMonster.DropTableId}).");
            _monsterView.DropTreeView.Nodes.Add(noDataNode);
            _monsterView.DropTreeView.EndUpdate();
            _monsterView.MonsterDataGrid.Enabled = true;
            _monsterView.ProgressBar.Visible = false;
            return;
        }

        foreach (Drop dropTable in dropTables)
        {
            TreeNode subIdNode = new($"Sub ID #{dropTable.SubId}");
            subIdNode.Tag = $"{dropTable.Id},{dropTable.SubId}";
            subIdNode.ContextMenuStrip = _monsterView.contextMenuStrip_SubId;

            for (int i = 0; i < 10; i++)
            {
                int dropId = dropTable.DropItemIds[i];

                if (dropId > 0)
                {
                    AddItemNode(subIdNode.Nodes, dropId, _monsterView.contextMenuStrip_TreeViewItem);
                }

                else if (dropId < 0)
                {
                    await AddDropGroupNode(
                        _dropRepository,
                        subIdNode.Nodes,
                        dropId,
                        dropTable.ItemNames[i],
                        _monsterView.contextMenuStrip_TreeViewItem);
                }

                else
                {
                    var emptyNode = new TreeNode("Empty slot");
                    emptyNode.Name = dropId.ToString();
                    emptyNode.ContextMenuStrip = _monsterView.contextMenuStrip_TreeViewItem;
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
        // DropGroup
        if (e.Node?.Tag is int dropId)
        {
            Drop? dropGroup = await _dropRepository.GetGroupById(dropId);

            if (dropGroup == null)
            {
                MessageBox.Show("No drop information found for the selected monster.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _monsterView.DropInfo.CurrentDrop = dropGroup;
        }
        // DropTable
        else if (e.Node?.Tag is string tagString)
        {
            int[] ids = tagString.Split(',').Select(int.Parse).ToArray();
            Drop? dropTable = await _dropRepository.GetTableByIdAndSubId(ids[0], ids[1]);

            if (dropTable == null)
            {
                MessageBox.Show("No drop information found for the selected monster.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _monsterView.DropInfo.CurrentDrop = dropTable;
        }
        // Empty or Item
        else if (e.Node?.Tag is null)
        {
            _monsterView.DropInfo.CurrentDrop = new();
        }
    }

    private async Task MonsterView_UpdateDrop(object? sender, EventArgs e)
    {
        Drop drop = _monsterView.DropInfo.CurrentDrop;
        if (drop == null)
        {
            MessageBox.Show("No DropGroup selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        int result;

        if (drop.SubId == -1)
        {
            var affectedMonsters = await _monsterRepository.GetByReferenceToDropGroupId(drop.Id);

            if (affectedMonsters.Count > 1)
            {
                _updateWarningView.SetDataSource(affectedMonsters);
                DialogResult dialogResult = _updateWarningView.ShowDialog();

                if (dialogResult != DialogResult.OK)
                    return;
            }

            result = await _dropRepository.UpdateGroup(drop);
        }
        else
            result = await _dropRepository.UpdateTable(drop);

        await UpdateDropTreeView();
        _monsterView.ShowResult($"{result} row(s) have been updated.");
    }

    #region DropInfo ContextMenu
    private void MonsterView_InfoRemoveDrop(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1 || _monsterView.DropInfo.CurrentDrop is null)
            return;

        Drop tempDrop = _monsterView.DropInfo.CurrentDrop;
        tempDrop.DropItemIds[e.ItemIndex] = 0;
        tempDrop.DropMaxCounts[e.ItemIndex] = 0;
        tempDrop.DropMinCounts[e.ItemIndex] = 0;
        tempDrop.DropPercentages[e.ItemIndex] = 0;
        tempDrop.ItemNames[e.ItemIndex] = "Empty slot";
        _monsterView.DropInfo.CurrentDrop = tempDrop;
    }

    private void MonsterView_InfoSetDropItem(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1 || _monsterView.DropInfo.CurrentDrop is null)
            return;

        var itemId = _setItemPresenter.OnShowView();

        if (itemId == 0)
            return;

        Drop tempDrop = _monsterView.DropInfo.CurrentDrop;
        tempDrop.DropItemIds[e.ItemIndex] = itemId;
        tempDrop.ItemNames[e.ItemIndex] = ItemCache.Cache[itemId];
        _monsterView.DropInfo.CurrentDrop = tempDrop;
    }

    private void MonsterView_InfoSetDropGroup(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1 || _monsterView.DropInfo.CurrentDrop is null)
            return;

        var dropGroupId = _setDropGroupPresenter.OnShowView();

        if (dropGroupId == 0)
            return;

        Drop tempDrop = _monsterView.DropInfo.CurrentDrop;
        var dropGroupName = DropGroupCache.Cache[dropGroupId].Alias;
        tempDrop.DropItemIds[e.ItemIndex] = dropGroupId;
        tempDrop.ItemNames[e.ItemIndex] = $"{dropGroupId}" + (dropGroupName != null && dropGroupName != string.Empty ? $": {dropGroupName}" : string.Empty);
        _monsterView.DropInfo.CurrentDrop = tempDrop;
    }

    private void MonsterView_InfoRenameDropGroup(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1 || _monsterView.DropInfo.CurrentDrop is null)
            return;

        var newName = _renameDropGroupPresenter.OnShowView();

        if (newName == null)
        {
            return;
        }

        int dropGroupId = _monsterView.DropInfo.CurrentDrop.DropItemIds[e.ItemIndex];
        DropGroupCache.AddDropGroupName(dropGroupId, newName);

        var nodes = _monsterView.DropTreeView.SelectedNode.Nodes.FindByName(dropGroupId.ToString(), true);

        foreach (var node in nodes)
        {
            node.Text = $"{dropGroupId}: {newName}";
        }

        Drop tempDrop = _monsterView.DropInfo.CurrentDrop;
        tempDrop.ItemNames[e.ItemIndex] = $"{dropGroupId}: {newName}";
        _monsterView.DropInfo.CurrentDrop = tempDrop;
    }

    private async Task MonsterView_InfoViewReferences(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1 || _monsterView.DropInfo.CurrentDrop == null)
            return;

        int dropId = _monsterView.DropInfo.CurrentDrop.DropItemIds[e.ItemIndex];

        if (dropId == 0)
            return;

        var affectedMonsters = dropId < 0
            ? await _monsterRepository.GetByReferenceToDropGroupId(dropId)
            : await _monsterRepository.GetByReferenceToItemId(dropId);

        _referencesView.SetMonsterDataSource(affectedMonsters);
        _referencesView.ShowDialog();
    }
    #endregion

    #region TreeView ContextMenu
    private void MonsterView_TreeRenameDropGroup(object? sender, TreeContextMenuEventArgs e)
    {
        if (!int.TryParse(e.SelectedNode.Name, out int dropGroupId))
            return;

        if (_renameDropGroupPresenter.OnShowView() is not string newName)
            return;

        DropGroupCache.AddDropGroupName(dropGroupId, newName);

        var nodesToRename = _monsterView.DropTreeView.Nodes.FindByName(dropGroupId.ToString(), true);
        foreach (var node in nodesToRename)
            node.Text = $"{dropGroupId}: {newName}";
    }

    private async Task MonsterView_TreeViewReferences(object? sender, TreeContextMenuEventArgs e)
    {
        if (!int.TryParse(e.SelectedNode.Name, out int dropId) || dropId == 0)
            return;

        var affectedMonsters = dropId < 0
            ? await _monsterRepository.GetByReferenceToDropGroupId(dropId)
            : await _monsterRepository.GetByReferenceToItemId(dropId);

        _referencesView.SetMonsterDataSource(affectedMonsters);
        _referencesView.ShowDialog();
    }

    private async Task MonsterView_AddSubId(object? sender, EventArgs e)
    {
        if (_monsterView.SelectedMonster is not Monster selectedMonster)
            return;

        MessageBox.Show($"DropTableId: {selectedMonster.DropTableId}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        // TODO
    }

    private async Task MonsterView_DeleteSubId(object? sender, TreeContextMenuEventArgs e)
    {
        if (e.SelectedNode.Tag is not string tag)
            return;

        MessageBox.Show(tag, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        // TODO
    }
    #endregion
}
