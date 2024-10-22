using Grace.Cache;
using Grace.Event.DropGroup;
using Grace.Model;
using Grace.Model.Repository;
using Grace.View;

namespace Grace.Presenter;

public class FilterDropGroupsPresenter
{
    private readonly FilterDropGroupsView _filterDropGroupsView;
    private readonly DropRepository _dropRepository;

    public FilterDropGroupsPresenter(FilterDropGroupsView filterDropGroupsView, DropRepository dropRepository)
    {
        _filterDropGroupsView = filterDropGroupsView;
        _dropRepository = dropRepository;
    }

    public async Task<List<Drop>?> OnShowView(DropGroupFilterType filterType)
    {
        List<Drop> filterResult = [];

        DialogResult dialogResult = _filterDropGroupsView.Open(filterType);

        if (dialogResult != DialogResult.OK)
            return null;

        string filterInput = _filterDropGroupsView.SearchInput;

        switch (filterType)
        {
            case DropGroupFilterType.ID:
                if (int.TryParse(filterInput, out int dropGroupId))
                    filterResult = DropGroupCache.Cache
                        .Where(v => v.Key == dropGroupId)
                        .Select(v => v.Value)
                        .ToList();
                break;
            case DropGroupFilterType.NAME:
                filterResult = DropGroupCache.Cache
                    .Where(v => v.Value.Alias.Contains(filterInput))
                    .Select(v => v.Value)
                    .ToList();
                break;
            case DropGroupFilterType.DROP_GROUP_ID:
                // TODO: solve with DropGroupCache
                if (int.TryParse(filterInput, out int referencedDropGroupId))
                {
                    var ids = (await _dropRepository.GetByReferenceToDropGroupId(referencedDropGroupId))
                        .Select(i => i.Id)
                        .ToList();

                    filterResult = DropGroupCache.Cache
                        .Where(v => ids.Contains(v.Key))
                        .Select(v => v.Value)
                        .ToList();
                }
                break;
            case DropGroupFilterType.ITEM_ID:
                // TODO: solve with DropGroupCache
                if (int.TryParse(filterInput, out int referencedItemId))
                {
                    var ids = (await _dropRepository.GetByReferenceToItemId(referencedItemId))
                        .Select(i => i.Id)
                        .ToList();

                    filterResult = DropGroupCache.Cache
                        .Where(v => ids.Contains(v.Key))
                        .Select(v => v.Value)
                        .ToList();
                }
                break;
        }

        return filterResult;

    }
}
