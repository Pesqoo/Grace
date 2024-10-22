using Grace.Cache;
using Grace.Event.SetDropGroup;
using Grace.View;

namespace Grace.Presenter;

public class SetDropGroupPresenter
{
    private SetDropGroupView _setDropGroupView;

    public SetDropGroupPresenter(SetDropGroupView setDropGroupView)
    {
        _setDropGroupView = setDropGroupView;
        _setDropGroupView.FilterDropGroupsEventHandler += SetDropGroupView_FilterDropGroups;
        _setDropGroupView.ResetDropGroupsEventHandler += SetDropGroupView_ResetDropGroups;
    }

    public int OnShowView()
    {
        _setDropGroupView.DropGroupDataGrid.DataSource = ItemCache.Cache
            .Where(p => p.Key < 0)
            .OrderBy(p => p.Key)
            .ToList();

        DialogResult dialogResult = _setDropGroupView.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            if (_setDropGroupView.DropGroupDataGrid.CurrentRow.Cells[0].Value is int dropGroupId)
                return dropGroupId;
        }

        return 0;
    }

    private void SetDropGroupView_FilterDropGroups(object? sender, FilterDropGroupsEventArgs e)
    {
        if (e.Value == string.Empty)
            return;

        if (e.FilterType == DropGroupFilterType.ID)
        {
            if (!int.TryParse(e.Value, out int id))
                return;

            _setDropGroupView.DropGroupDataGrid.DataSource = ItemCache.Cache
                .Where(p => p.Key == id)
                .ToList();
        }
        else if (e.FilterType == DropGroupFilterType.NAME)
        {
            _setDropGroupView.DropGroupDataGrid.DataSource = ItemCache.Cache
                .Where(p => p.Key < 0 && p.Value.Contains(e.Value))
                .OrderBy(p => p.Key)
                .ToList();
        }
    }

    private void SetDropGroupView_ResetDropGroups(object? sender, EventArgs e)
    {
        _setDropGroupView.DropGroupDataGrid.DataSource = ItemCache.Cache
            .Where(p => p.Key < 0)
            .OrderBy(p => p.Key)
            .ToList();
    }
}
