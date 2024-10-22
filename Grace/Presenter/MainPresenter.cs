using Grace.Cache;
using Grace.View;

namespace Grace.Presenter;
public class MainPresenter
{
    private readonly MonsterCache _monsterCache;
    private readonly ItemCache _itemCache;
    private readonly DropGroupCache _dropGroupCache;

    private readonly MainView _mainView;
    private readonly MonsterView _monsterView;
    private readonly DropGroupsView _dropGroupView;

    public MainPresenter(
        MonsterCache monsterCache,
        ItemCache itemCache,
        DropGroupCache dropGroupCache,
        MainView mainView,
        MonsterView monsterView,
        DropGroupsView dropGroupView)
    {
        _monsterCache = monsterCache;
        _itemCache = itemCache;
        _dropGroupCache = dropGroupCache;
        Task.Run(MainView_OnLoad).Wait();

        _mainView = mainView;
        _monsterView = monsterView;
        _dropGroupView = dropGroupView;

        _monsterView.AttachToParent(_mainView.MonstersTab);
        _dropGroupView.AttachToParent(_mainView.DropGroupsTab);
    }

    private async Task MainView_OnLoad()
    {
        await _monsterCache.Init();
        await _itemCache.Init();
        await _dropGroupCache.Init();
    }
}
