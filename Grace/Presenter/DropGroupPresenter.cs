using Grace.Cache;
using Grace.Event.ContextMenu;
using Grace.Event.DropGroup;
using Grace.Model;
using Grace.Model.Repository;
using Grace.View;
using static Grace.Common.Util;

namespace Grace.Presenter;
public class DropGroupPresenter
{
    private readonly DropGroupsView _dropGroupView;
    private readonly UpdateWarningView _updateWarningView;
    private readonly References _referencesView;
    private readonly FilterDropGroupsPresenter _filterDropGroupsPresenter;
    private readonly SetItemPresenter _setItemPresenter;
    private readonly SetDropGroupPresenter _setDropGroupPresenter;
    private readonly RenameDropGroupPresenter _renameDropGroupPresenter;
    private readonly ItemCache _itemCache;
    private readonly DropGroupCache _dropGroupCache;
    private readonly DropRepository _dropRepository;
    private readonly MonsterRepository _monsterRepository;

    public DropGroupPresenter(
        DropGroupsView dropGroupView,
        UpdateWarningView updateWarningView,
        References referencesView,
        FilterDropGroupsPresenter filterDropGroupsPresenter,
        SetItemPresenter setItemPresenter,
        SetDropGroupPresenter setDropGroupPresenter,
        RenameDropGroupPresenter renameDropGroupPresenter,
        ItemCache itemCache,
        DropGroupCache dropGroupCache,
        DropRepository dropRepository,
        MonsterRepository monsterRepository)
    {
        _dropGroupView = dropGroupView;
        _updateWarningView = updateWarningView;
        _referencesView = referencesView;
        _filterDropGroupsPresenter = filterDropGroupsPresenter;
        _setItemPresenter = setItemPresenter;
        _setDropGroupPresenter = setDropGroupPresenter;
        _renameDropGroupPresenter = renameDropGroupPresenter;
        _itemCache = itemCache;
        _dropGroupCache = dropGroupCache;
        _dropRepository = dropRepository;
        _monsterRepository = monsterRepository;

        _dropGroupView.Load += async (sender, e) => await DropGroupView_LoadDropGroups(sender, new LoadDropGroupsEventArgs(false));

        _dropGroupView.LoadDropGroupsEventHandler += async (sender, e) => await DropGroupView_LoadDropGroups(sender, e);
        _dropGroupView.SelectDropGroupEventHandler += async (sender, e) => await UpdateDropTreeView();
        _dropGroupView.FilterDropGroupsEventHandler += async (sender, e) => await DropGroupView_FilterDropGroups(sender, e);
        _dropGroupView.SelectDropEventHandler += async (sender, e) => await DropGroupView_SelectNestedDropGroup(sender, e);
        _dropGroupView.SaveDropEventHandler += async (sender, e) => await DropGroupView_UpdateDrop(sender, e);

        _dropGroupView.RemoveItemEventHandler += DropGroupView_RemoveDrop;
        _dropGroupView.SetItemEventHandler += DropGroupView_SetDropItem;
        _dropGroupView.SetDropGroupEventHandler += DropGroupView_SetDropGroup;
        _dropGroupView.RenameDropGroupEventHandler += DropGroupView_RenameDropGroup;
        _dropGroupView.ViewReferencesEventHandler += async (sender, e) => await DropGroupView_InfoViewReferences(sender, e);

        // TreeView ContextMenu
        _dropGroupView.TreeRenameDropGroupEventHandler += DropGroupView_TreeRenameDropGroup;
        _dropGroupView.TreeViewReferencesEventHandler += async (sender, e) => await DropGroupView_TreeViewReferences(sender, e);

        // DataGrid ContextMenu
        _dropGroupView.DataGridRenameDropGroupEventHandler += DropGroupView_DataGridRenameDropGroup;
        _dropGroupView.DataGridViewReferencesEventHandler += async (sender, e) => await DropGroupView_DataGridViewReferences(sender, e);

    }

    private async Task DropGroupView_LoadDropGroups(object? sender, LoadDropGroupsEventArgs e)
    {
        if (e.ReloadCache)
        {
            await _itemCache.Init();
            await _dropGroupCache.Init();
        }

        var dropGroups = DropGroupCache.Cache.Values.ToList();

        _dropGroupView.SetDropGroupDataSource(dropGroups);
        await UpdateDropTreeView();
    }

    private async Task DropGroupView_FilterDropGroups(object? sender, Event.DropGroup.FilterDropGroupsEventArgs e)
    {
        List<Drop>? filterResult;
        if (e.FilterType == Event.DropGroup.DropGroupFilterType.ALL)
        {
            filterResult = DropGroupCache.Cache.Values.ToList();
        }
        else
        {
            filterResult = await _filterDropGroupsPresenter.OnShowView(e.FilterType);
            if (filterResult == null)
                return;
        }

        _dropGroupView.SetDropGroupDataSource(filterResult);
        await UpdateDropTreeView();
    }

    private async Task UpdateDropTreeView()
    {
        if (_dropGroupView.SelectedDrop is not Drop drop)
        {
            _dropGroupView.DropTreeView.Nodes.Clear();
            _dropGroupView.CurrentDrop = new();
            return;
        }

        _dropGroupView.CurrentDrop = drop;

        // _dropGroupView.dropGroupDataView.Enabled = false;
        _dropGroupView.ProgressBar.Visible = true;

        Drop? dropGroup = await _dropRepository.GetGroupById(drop.Id);

        if (dropGroup == null)
            return;

        _dropGroupView.DropTreeView.Nodes.Clear();
        _dropGroupView.DropTreeView.BeginUpdate();

        for (int i = 0; i < 10; i++)
        {
            // DropGroup < 0 < Item
            int dropId = dropGroup.DropItemIds[i];

            // TODO: ContextMenu
            if (dropId > 0)
            {
                AddItemNode(_dropGroupView.DropTreeView.Nodes, dropId, _dropGroupView.contextMenuStrip_TreeViewItem);
            }
            else if (dropId < 0)
            {
                await AddDropGroupNode(
                    _dropRepository,
                    _dropGroupView.DropTreeView.Nodes,
                    dropId,
                    dropGroup.ItemNames[i],
                    _dropGroupView.contextMenuStrip_TreeViewItem);
            }
            else
            {
                _dropGroupView.DropTreeView.Nodes.Add(new TreeNode("Empty slot"));
            }
        }

        _dropGroupView.DropTreeView.EndUpdate();
        _dropGroupView.ProgressBar.Visible = false;
        // _dropGroupView.dropGroupDataView.Enabled = true;
    }

    private async Task DropGroupView_SelectNestedDropGroup(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is not int dropId)
        {
            _dropGroupView.CurrentDrop = new();
            return;
        }

        if (!DropGroupCache.Cache.TryGetValue(dropId, out Drop? dropGroup))
        {
            MessageBox.Show("No drop information found for the selected monster.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _dropGroupView.CurrentDrop = dropGroup;
    }

    private async Task DropGroupView_UpdateDrop(object? sender, EventArgs e)
    {
        Drop drop = _dropGroupView.CurrentDrop;

        var affectedMonsters = await _monsterRepository.GetByReferenceToDropGroupId(drop.Id);

        if (affectedMonsters.Count > 1)
        {
            _updateWarningView.SetDataSource(affectedMonsters);
            DialogResult dialogResult = _updateWarningView.ShowDialog();

            if (dialogResult != DialogResult.OK)
                return;
        }

        int result = await _dropRepository.UpdateGroup(drop);

        await UpdateDropTreeView();
        _dropGroupView.ShowResult($"{result} row(s) have been updated.");
    }

    #region DropInfo ContextMenu
    private void DropGroupView_RemoveDrop(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        Drop tempDrop = _dropGroupView.CurrentDrop;
        tempDrop.DropItemIds[e.ItemIndex] = 0;
        tempDrop.DropMaxCounts[e.ItemIndex] = 0;
        tempDrop.DropMinCounts[e.ItemIndex] = 0;
        tempDrop.DropPercentages[e.ItemIndex] = 0;
        tempDrop.ItemNames[e.ItemIndex] = "Empty slot";
        _dropGroupView.CurrentDrop = tempDrop;
    }

    private void DropGroupView_SetDropItem(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        var itemId = _setItemPresenter.OnShowView();

        if (itemId == 0)
            return;

        Drop tempDrop = _dropGroupView.CurrentDrop;
        tempDrop.DropItemIds[e.ItemIndex] = itemId;
        tempDrop.ItemNames[e.ItemIndex] = ItemCache.Cache[itemId];
        _dropGroupView.CurrentDrop = tempDrop;
    }

    private void DropGroupView_SetDropGroup(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        var dropGroupId = _setDropGroupPresenter.OnShowView();

        if (dropGroupId == 0)
            return;

        Drop tempDrop = _dropGroupView.CurrentDrop;
        var dropGroupName = DropGroupCache.Cache[dropGroupId].Alias;
        tempDrop.DropItemIds[e.ItemIndex] = dropGroupId;
        tempDrop.ItemNames[e.ItemIndex] = $"{dropGroupId}" + (dropGroupName != null && dropGroupName != string.Empty ? $": {dropGroupName}" : string.Empty);
        _dropGroupView.CurrentDrop = tempDrop;
    }

    private void DropGroupView_RenameDropGroup(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        var newName = _renameDropGroupPresenter.OnShowView();

        if (newName == null)
        {
            return;
        }

        int dropGroupId = _dropGroupView.CurrentDrop.DropItemIds[e.ItemIndex];
        DropGroupCache.AddDropGroupName(dropGroupId, newName);

        var nodes = _dropGroupView.DropTreeView.SelectedNode.Nodes.FindByName(dropGroupId.ToString(), true);

        foreach (var node in nodes)
        {
            node.Text = $"{dropGroupId}: {newName}";
        }

        Drop tempDrop = _dropGroupView.CurrentDrop;
        tempDrop.ItemNames[e.ItemIndex] = $"{dropGroupId}: {newName}";
        _dropGroupView.CurrentDrop = tempDrop;
    }

    private async Task DropGroupView_InfoViewReferences(object? sender, InfoContextMenuEventArgs e)
    {
        if (e.ItemIndex == -1)
            return;

        int dropId = _dropGroupView.CurrentDrop.DropItemIds[e.ItemIndex];

        if (dropId == 0)
            return;

        var affectedMonsters = dropId < 0
            ? await _monsterRepository.GetByReferenceToDropGroupId(dropId)
            : await _monsterRepository.GetByReferenceToItemId(dropId);

        _referencesView.SetDataSource(affectedMonsters);
        _referencesView.ShowDialog();
    }
    #endregion

    public void DropGroupView_TreeRenameDropGroup(object? sender, TreeContextMenuEventArgs e)
    {
        if (!int.TryParse(e.SelectedNode.Name, out int dropGroupId))
            return;

        if (_renameDropGroupPresenter.OnShowView() is not string newName)
            return;

        DropGroupCache.AddDropGroupName(dropGroupId, newName);
        _dropGroupView.DataGridView.Refresh();

        var nodesToRename = _dropGroupView.DropTreeView.Nodes.FindByName(dropGroupId.ToString(), true);
        foreach (var node in nodesToRename)
            node.Text = $"{dropGroupId}: {newName}";
    }

    private async Task DropGroupView_TreeViewReferences(object? sender, TreeContextMenuEventArgs e)
    {
        if (!int.TryParse(e.SelectedNode.Name, out int dropId) || dropId == 0)
            return;

        var affectedMonsters = dropId < 0
            ? await _monsterRepository.GetByReferenceToDropGroupId(dropId)
            : await _monsterRepository.GetByReferenceToItemId(dropId);

        _referencesView.SetDataSource(affectedMonsters);
        _referencesView.ShowDialog();
    }

    public void DropGroupView_DataGridRenameDropGroup(object? sender, EventArgs e)
    {
        if (_dropGroupView.SelectedDrop is not Drop drop)
            return;

        if (_renameDropGroupPresenter.OnShowView() is not string newName)
            return;

        DropGroupCache.AddDropGroupName(drop.Id, newName);
        _dropGroupView.DataGridView.Refresh();

        var nodesToRename = _dropGroupView.DropTreeView.Nodes.FindByName(drop.Id.ToString(), true);
        foreach (var node in nodesToRename)
            node.Text = $"{drop.Id}: {newName}";
    }

    public async Task DropGroupView_DataGridViewReferences(object? sender, EventArgs e)
    {
        if (_dropGroupView.SelectedDrop is not Drop drop)
            return;

        var affectedMonsters = drop.Id < 0
            ? await _monsterRepository.GetByReferenceToDropGroupId(drop.Id)
            : await _monsterRepository.GetByReferenceToItemId(drop.Id);

        _referencesView.SetDataSource(affectedMonsters);
        _referencesView.ShowDialog();
    }
}
