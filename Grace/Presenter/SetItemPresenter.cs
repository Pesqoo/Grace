using Grace.Cache;
using Grace.Event;
using Grace.View;

namespace Grace.Presenter;

public class SetItemPresenter
{
    private SetItemView _setItemView;

    public SetItemPresenter(SetItemView setItemView)
    {
        _setItemView = setItemView;
        _setItemView.FilterItemsEventHandler += SetItemView_FilterItems;
        _setItemView.ResetItemsEventHandler += SetItemView_ResetItems;
    }

    public int OnShowView()
    {
        _setItemView.ItemDataGrid.DataSource = ItemCache.Cache
            .Where(p => p.Key > 0)
            .OrderBy(p => p.Key)
            .ToList();

        DialogResult dialogResult = _setItemView.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            if (_setItemView.ItemDataGrid.CurrentRow.Cells[0].Value is int itemId)
                return itemId;
        }

        return 0;
    }

    private void SetItemView_FilterItems(object? sender, FilterItemsEventArgs e)
    {
        if (e.Value == string.Empty)
            return;

        if (e.FilterType == ItemFilterType.ID)
        {
            if (!int.TryParse(e.Value, out int id))
                return;

            _setItemView.ItemDataGrid.DataSource = ItemCache.Cache
                .Where(p => p.Key == id)
                .ToList();
        }
        else if (e.FilterType == ItemFilterType.NAME)
        {
            _setItemView.ItemDataGrid.DataSource = ItemCache.Cache
                .Where(p => p.Key > 0 && p.Value.Contains(e.Value))
                .OrderBy(p => p.Key)
                .ToList();
        }
    }

    private void SetItemView_ResetItems(object? sender, EventArgs e)
    {
        _setItemView.ItemDataGrid.DataSource = ItemCache.Cache
            .Where(p => p.Key > 0)
            .OrderBy(p => p.Key)
            .ToList();
    }
}
