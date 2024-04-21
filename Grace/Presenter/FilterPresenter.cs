using Grace.Event;
using Grace.Model;
using Grace.Model.Repository;
using Grace.View;

namespace Grace.Presenter;

public class FilterPresenter
{
    private FilterView _filterView;
    private MonsterRepository _monsterRepository;

    public FilterPresenter(FilterView filterView, MonsterRepository monsterRepository)
    {
        _filterView = filterView;
        _monsterRepository = monsterRepository;
    }

    public async Task<List<Monster>?> OnShowView(MonsterFilterType filterType)
    {
        List<Monster> filterResult = new();

        DialogResult dialogResult = _filterView.Open(filterType);

        if (dialogResult == DialogResult.OK)
        {
            string filterInput = _filterView.SearchInput;

            switch (filterType)
            {
                case MonsterFilterType.ID:
                    if (int.TryParse(filterInput, out int monsterId))
                    {
                        filterResult = await _monsterRepository.GetById(monsterId);
                    }
                    break;
                case MonsterFilterType.NAME:
                    filterResult = await _monsterRepository.GetByName(filterInput);
                    break;
                case MonsterFilterType.LOCATION:
                    filterResult = await _monsterRepository.GetByLocation(filterInput);
                    break;
                case MonsterFilterType.DROP_ID:
                    if (int.TryParse(filterInput, out int dropId))
                    {
                        filterResult = await _monsterRepository.GetByDropId(dropId);
                    }
                    break;
            }

            return filterResult;
        }

        return null;
    }
}
