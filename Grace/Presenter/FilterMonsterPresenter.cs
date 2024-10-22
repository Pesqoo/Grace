using Grace.Cache;
using Grace.Event;
using Grace.Model;
using Grace.Model.Repository;
using Grace.View;

namespace Grace.Presenter;

public class FilterMonsterPresenter(
    FilterView filterView,
    MonsterView monsterView,
    MonsterRepository monsterRepository)
{
    private readonly FilterView _filterView = filterView;
    private readonly MonsterView _monsterView = monsterView;
    private readonly MonsterRepository _monsterRepository = monsterRepository;

    public async Task<List<Monster>?> OnShowView(MonsterFilterType filterType)
    {
        List<Monster> filterResult = [];

        DialogResult dialogResult = _filterView.Open(filterType);

        if (dialogResult != DialogResult.OK)
            return null;

        string filterInput = _filterView.SearchInput;

        switch (filterType)
        {
            case MonsterFilterType.ID:
                if (int.TryParse(filterInput, out int monsterId))
                    filterResult = MonsterCache.Cache
                        .Where(v => v.Id == monsterId)
                        .ToList();
                break;
            case MonsterFilterType.NAME:
                filterResult = MonsterCache.Cache
                    .Where(v => v.Name != null && v.Name.Contains(filterInput))
                    .ToList();
                break;
            case MonsterFilterType.LOCATION:
                filterResult = MonsterCache.Cache
                    .Where(v => v.Location != null && v.Location.Contains(filterInput))
                    .ToList();
                break;
            case MonsterFilterType.DROP_TABLE_ID:
                if (int.TryParse(filterInput, out int dropTableId))
                    filterResult = MonsterCache.Cache
                        .Where(v => v.DropTableId == dropTableId)
                        .ToList();
                break;
            case MonsterFilterType.DROP_GROUP_ID:
                // TODO: solve with MonsterCache
                if (int.TryParse(filterInput, out int dropGroupId))
                    filterResult = await _monsterRepository.GetByReferenceToDropGroupId(dropGroupId);
                break;
            case MonsterFilterType.ITEM_ID:
                // TODO: solve with MonsterCache
                if (int.TryParse(filterInput, out int itemId))
                    filterResult = await _monsterRepository.GetByReferenceToItemId(itemId);
                break;
        }

        if (_monsterView.HideMonstersWithoutTable)
            filterResult = filterResult.Where(v => v.DropTableId != 0).ToList();

        return filterResult;
    }
}
