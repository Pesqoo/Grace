using Grace.Cache;
using Grace.Event;
using Grace.Model;
using Grace.Model.DataContext;
using Grace.Model.Repository;
using Grace.View;
using System.Diagnostics;
using System.Text;

namespace Grace.Presenter;

public class MainPresenter
{
    private readonly MainView _mainView;
    private readonly FilterView _filterView;
    private readonly FilterPresenter _filterPresenter;
    private List<Monster> _monsters = [];

    public MainPresenter(MainView mainView)
    {
        _mainView = mainView;
        _filterView = new FilterView();
        _filterPresenter = new FilterPresenter(_filterView);

        _mainView.Load += async (sender, e) => await ItemCache.Init();
        _mainView.Load += async (sender, e) => await MonsterCache.Init();

        _mainView.LoadMonstersEventHandler += MainView_LoadMonsters;
        _mainView.FilterMonstersEventHandler += MainView_FilterMonsters;
        _mainView.SelectMonsterEventHandler += async (sender, e) => await MainView_SelectMonster(sender, e);
        _mainView.SelectDropEventHandler += async (sender, e) => await MainView_SelectDrop(sender, e);
        _mainView.SaveDropEventHandler += async (sender, e) => await MainView_UpdateDrop(sender, e);
    }

    private void MainView_LoadMonsters(object? sender, EventArgs e)
    {
        _monsters = MonsterCache.Cache;
        _mainView.MonsterDataGrid.DataSource = _monsters;
    }

    private async void MainView_FilterMonsters(object? sender, FilterMonstersEventArgs e)
    {
        if (e.FilterType == MonsterFilterType.ALL)
        {
            _monsters = MonsterCache.Cache;
        }
        else
        {
            object result = await _filterPresenter.OnShowView(e.FilterType);
            if (result is List<Monster> list)
            {
                _monsters = list;
            }
        }

        _mainView.MonsterDataGrid.DataSource = _monsters;
    }

    // TODO: quickly selecting two monsters results in adding subIdNodes of both monsters to the TreeView
    private async Task MainView_SelectMonster(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1)
            return;

        _mainView.ProgressBar.Visible = true;

        Monster selectedMonster = _monsters[e.RowIndex];
        List<DropTable> dropTables = await DropTableRepository.GetById(selectedMonster.Id);

        _mainView.DropTreeView.Nodes.Clear();
        _mainView.DropTreeView.BeginUpdate();

        if (dropTables.Count == 0)
        {
            TreeNode noDataNode = new("No drop data available for this Droplink ID.");
            _mainView.DropTreeView.Nodes.Add(noDataNode);
            _mainView.DropTreeView.EndUpdate();
            _mainView.ProgressBar.Visible = false;
            return;
        }

        foreach (DropTable dropTable in dropTables)
        {
            TreeNode subIdNode = new($"Sub ID #{dropTable.SubId}")
            {
                Name = dropTable.SubId.ToString(),
                Tag = $"{dropTable.Id},{dropTable.SubId}"
            };

            foreach (int dropId in dropTable.DropItemIds)
            {
                if (dropId > 0)
                    AddItemNode(subIdNode, dropId);

                else if (dropId < 0)
                    await AddDropGroupNode(subIdNode, dropId);

                else
                    subIdNode.Nodes.Add(new TreeNode("Empty slot"));
            }

            _mainView.DropTreeView.Nodes.Add(subIdNode);
        }

        _mainView.DropTreeView.EndUpdate();
        _mainView.ProgressBar.Visible = false;
    }

    private static void AddItemNode(TreeNode parentNode, int itemId)
    {
        string itemName = ItemCache.Cache[itemId];
        TreeNode itemNode = new($"Item: {itemName} (ID: {itemId})");
        parentNode.Nodes.Add(itemNode);
    }

    private async Task AddDropGroupNode(TreeNode parentNode, int dropGroupId)
    {
        TreeNode groupNode = new($"Drop Group: ID {dropGroupId}");
        parentNode.Nodes.Add(groupNode);
        groupNode.Tag = dropGroupId;

        DropGroup? dropGroup = await DropGroupRepository.GetById(dropGroupId);
        if (dropGroup == null)
        {
            TreeNode noDataNode = new("No drops found");
            groupNode.Nodes.Add(noDataNode);

            return;
        }

        foreach (int dropId in dropGroup.DropItemIds)
        {
            if (dropId > 0)
                AddItemNode(groupNode, dropId);

            else if (dropId < 0)
                await AddDropGroupNode(groupNode, dropId);

            else
                groupNode.Nodes.Add(new TreeNode("Empty slot"));
        }
    }

    private async Task MainView_SelectDrop(object? sender, TreeViewEventArgs e)
    {
        // load dropGroup
        if (e.Node?.Tag is int dropId)
        {
            DropGroup? dropGroup = await DropGroupRepository.GetById(dropId);

            if (dropGroup == null)
            {
                MessageBox.Show("No drop information found for the selected monster.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _mainView.DropGroupId.Text = dropId.ToString();
            _mainView.DropGroupId.Tag = -1;

            UpdateDropGroupControls(dropGroup);
        }
        else if (e.Node?.Tag is string tagString)
        {
            int[] ids = tagString.Split(',').Select(int.Parse).ToArray();
            DropTable? dropTable = await DropTableRepository.GetByIdAndSubId(ids[0], ids[1]);

            if (dropTable == null)
            {
                MessageBox.Show("No drop information found for the selected monster.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _mainView.DropGroupId.Text = ids[0].ToString();
            _mainView.DropGroupId.Tag = ids[1];

            UpdateDropGroupControls(dropTable);
        }
    }

    private void UpdateDropGroupControls(DropGroup dropGroup)
    {
        for (int i = 0; i < 10; i++)
        {
            // item names
            int itemId = dropGroup.DropItemIds[i];
            string itemName = "Empty slot";
            TextBox textBox_ItemName = _mainView.DropNames[i];

            if (itemId > 0)
                itemName = ItemCache.Cache[itemId];

            else if (itemId < 0)
                itemName = $"Group: {itemId}";

            textBox_ItemName.Tag = itemId;
            textBox_ItemName.Text = itemName;


            // drop counts
            NumericUpDown upDown_dropMinCount = _mainView.DropMinCounts[i];
            NumericUpDown upDown_dropMaxCount = _mainView.DropMaxCounts[i];
            upDown_dropMinCount.Value = dropGroup.DropMinCounts[i];
            upDown_dropMaxCount.Value = dropGroup.DropMaxCounts[i];

            // drop percentage
            MaskedTextBox textBox_DropChance = _mainView.DropChances[i];
            textBox_DropChance.Text = (dropGroup.DropPercentages[i]).ToString("0.00000000");


            // total percentage
            Label label_TotalPercentage = _mainView.TotalDropPercentage;
            double sum = 0.0;
            foreach (double dropChance in dropGroup.DropPercentages)
                sum += dropChance;

            string sumString = (sum).ToString("0.00");
            label_TotalPercentage.Text = $"Total: {sumString}";
        }
    }

    private async Task MainView_UpdateDrop(object? sender, EventArgs e)
    {
        DropTable dropTable = new DropTable();
        dropTable.Id = Convert.ToInt32(_mainView.DropGroupId.Text);
        dropTable.SubId = Convert.ToInt32(_mainView.DropGroupId.Tag);

        string tableName = dropTable.Id < 0 ? "DropGroupResource" : "MonsterDropTableResource";
        var queryBuilder = new StringBuilder($"UPDATE {tableName} SET ");

        for (int i = 0; i < 10; i++)
        {
            dropTable.DropItemIds[i] = Convert.ToInt32(_mainView.DropNames[i].Tag);
            dropTable.DropMinCounts[i] = Convert.ToInt32(_mainView.DropMinCounts[i].Value);
            dropTable.DropMaxCounts[i] = Convert.ToInt32(_mainView.DropMaxCounts[i].Value);
            dropTable.DropPercentages[i] = Convert.ToDouble(_mainView.DropChances[i].Text);

            queryBuilder.Append($"drop_item_id_{i:00} = {dropTable.DropItemIds[i]}, ");
            queryBuilder.Append($"drop_min_count_{i:00} = {dropTable.DropMinCounts[i]}, ");
            queryBuilder.Append($"drop_max_count_{i:00} = {dropTable.DropMaxCounts[i]}, ");
            queryBuilder.Append($"drop_percentage_{i:00} = {dropTable.DropPercentages[i].ToString(System.Globalization.CultureInfo.InvariantCulture)}, ");
        }

        queryBuilder.Length -= 2;

        queryBuilder.Append($" WHERE id = {dropTable.Id}");

        if (dropTable.Id > 0)
            queryBuilder.Append($" AND sub_id = {dropTable.SubId}");

        Debug.WriteLine(queryBuilder.ToString());

        await DBManager.ExecuteNonQueryAsync(queryBuilder.ToString());
        _mainView.ShowResult("Successfully saved");
    }
}


