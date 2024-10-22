using Grace.Cache;
using Grace.Event;
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
    private readonly FilterDropGroupsPresenter _filterDropGroupsPresenter;
    private readonly SetItemPresenter _setItemPresenter;
    private readonly SetDropGroupPresenter _setDropGroupPresenter;
    private readonly RenameDropGroupPresenter _renameDropGroupPresenter;
    private readonly ItemCache _itemCache;
    private readonly DropGroupCache _dropGroupCache;
    private readonly DropRepository _dropRepository;
    private readonly MonsterRepository _monsterRepository;
    private List<Drop> _dropGroups = [];

    public DropGroupPresenter(
        DropGroupsView dropGroupView,
        UpdateWarningView updateWarningView,
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
        _dropGroupView.SelectDropGroupEventHandler += async (sender, e) => await DropGroupView_SelectDropGroup(sender, e);
        _dropGroupView.FilterDropGroupsEventHandler += async (sender, e) => await DropGroupView_FilterDropGroups(sender, e);
        _dropGroupView.SelectDropEventHandler += async (sender, e) => await DropGroupView_SelectNestedDropGroup(sender, e);
        _dropGroupView.SaveDropEventHandler += async (sender, e) => await DropGroupView_UpdateDrop(sender, e);
        _dropGroupView.RemoveItemEventHandler += DropGroupView_RemoveDrop;
        _dropGroupView.SetItemEventHandler += DropGroupView_SetDropItem;
        _dropGroupView.SetDropGroupEventHandler += DropGroupView_SetDropGroup;
        _dropGroupView.RenameDropGroupEventHandler += DropGroupView_RenameDropGroup;

    }

    private async Task DropGroupView_LoadDropGroups(object? sender, LoadDropGroupsEventArgs e)
    {
        if (e.ReloadData)
        {
            await _itemCache.Init();
            await _dropGroupCache.Init();
        }

        _dropGroups = DropGroupCache.Cache.Values.ToList();
        _dropGroupView.SetDropGroupDataSource(_dropGroups);

        if (_dropGroups.Count > 0)
            await DropGroupView_SelectDropGroup(this, new DataGridViewCellEventArgs(0, 0));
    }

    private async Task DropGroupView_FilterDropGroups(object? sender, FilterDropGroupsEventArgs e)
    {
        if (e.FilterType == DropGroupFilterType.ALL)
        {
            _dropGroups = DropGroupCache.Cache.Values.ToList();
        }
        else
        {
            List<Drop>? result = await _filterDropGroupsPresenter.OnShowView(e.FilterType);
            if (result != null)
            {
                _dropGroups = result;
            }
        }

        _dropGroupView.SetDropGroupDataSource(_dropGroups);
        if (_dropGroups.Count > 0)
            await DropGroupView_SelectDropGroup(this, new DataGridViewCellEventArgs(0, 0));
        else
        {
            _dropGroupView.DropTreeView.Nodes.Clear();
            _dropGroupView.CurrentDrop = new();
        }
    }

    private async Task DropGroupView_SelectDropGroup(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1)
            return;

        var dropGroup = await _dropRepository.GetGroupById(_dropGroups[e.RowIndex].Id);
        if (dropGroup == null)
            return;

        _dropGroupView.CurrentDrop = dropGroup;
        await UpdateDropTreeView(dropGroup);
    }

    private async Task UpdateDropTreeView(Drop dropGroup)
    {
        _dropGroupView.ProgressBar.Visible = true;
        _dropGroupView.DropTreeView.Nodes.Clear();
        _dropGroupView.DropTreeView.BeginUpdate();

        for (int i = 0; i < 10; i++)
        {
            // DropGroup < 0 < Item
            int dropId = dropGroup.DropItemIds[i];

            if (dropId > 0)
            {
                var itemNode = CreateItemNode(dropId);
                _dropGroupView.DropTreeView.Nodes.Add(itemNode);
            }
            else if (dropId < 0)
            {
                // TODO: if alias is null, an invalid DropGroup was referenced
                DropGroupCache.Cache.TryGetValue(dropId, out var cachedDropGroup);
                var alias = cachedDropGroup?.Alias;

                var dropGroupName = $"{dropId}" + (alias != null && alias != string.Empty ? $": {alias}" : string.Empty);
                var groupNode = await CreateDropGroupNode(_dropRepository, dropId, dropGroupName);

                _dropGroupView.DropTreeView.Nodes.Add(groupNode);
            }
            else
            {
                _dropGroupView.DropTreeView.Nodes.Add(new TreeNode("Empty slot"));
            }
        }

        _dropGroupView.DropTreeView.EndUpdate();
        _dropGroupView.ProgressBar.Visible = false;
    }

    private async Task DropGroupView_SelectNestedDropGroup(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is not int dropId)
            return;

        Drop? dropGroup = await _dropRepository.GetGroupById(dropId);

        if (dropGroup == null)
        {
            MessageBox.Show("No drop information found for the selected monster.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _dropGroupView.CurrentDrop = dropGroup;
    }

    private async Task DropGroupView_UpdateDrop(object? sender, EventArgs e)
    {
        Drop drop = _dropGroupView.CurrentDrop;
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

            var index = _dropGroups.FindIndex(v => v.Id == drop.Id);
            if (index == -1)
                _dropGroups[index] = drop;
        }
        else
            result = await _dropRepository.UpdateTable(drop);

        await UpdateDropTreeView(drop);
        _dropGroupView.ShowResult($"{result} row(s) have been updated.");
    }

    private void DropGroupView_RemoveDrop(object? sender, ContextMenuEventArgs e)
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

    private void DropGroupView_SetDropItem(object? sender, ContextMenuEventArgs e)
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

    private void DropGroupView_SetDropGroup(object? sender, ContextMenuEventArgs e)
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

    private void DropGroupView_RenameDropGroup(object? sender, ContextMenuEventArgs e)
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

        var nodes = _dropGroupView.DropTreeView.SelectedNode.Nodes.FindByName(dropGroupId.ToString());

        foreach (var node in nodes)
        {
            node.Text = $"{dropGroupId}: {newName}";
        }

        Drop tempDrop = _dropGroupView.CurrentDrop;
        tempDrop.ItemNames[e.ItemIndex] = $"{dropGroupId}: {newName}";
        _dropGroupView.CurrentDrop = tempDrop;
    }
}
