using Grace.Event;
using Grace.Model;
using Grace.Model.Repository;
using Grace.View;

namespace Grace.Presenter;

public class FilterPresenter
{
    private FilterView _filterView;

    public FilterPresenter(FilterView filterView)
    {
        _filterView = filterView;
    }

    public async Task<object> OnShowView(MonsterFilterType filterType)
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
                        filterResult = await MonsterRepository.GetById(monsterId);
                    }
                    break;
                case MonsterFilterType.NAME:
                    filterResult = await MonsterRepository.GetByName(filterInput);
                    break;
                case MonsterFilterType.LOCATION:
                    filterResult = await MonsterRepository.GetByLocation(filterInput);
                    break;
                case MonsterFilterType.DROP_ID:
                    if (int.TryParse(filterInput, out int dropId))
                    {
                        filterResult = await MonsterRepository.GetByDropId(dropId);
                    }
                    break;
            }

            return filterResult;
        }

        return dialogResult;
    }
}
