using Grace.Cache;
using Grace.Config;
using Grace.Model.DataContext;
using Grace.Model.Repository;
using Grace.Presenter;
using Grace.View;
using Microsoft.Extensions.DependencyInjection;

namespace Grace;

internal static class Program
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        ConfigureServices();

        Application.Run(ServiceProvider!.GetRequiredService<MainView>());
    }

    static void ConfigureServices()
    {
        var services = new ServiceCollection()
            .AddSingleton<MainView>()
            .AddSingleton<DropGroupsView>()
            .AddSingleton<MonsterView>()
            .AddSingleton<FilterView>()
            .AddSingleton<SetItemView>()
            .AddSingleton<SetDropGroupView>()
            .AddSingleton<FilterDropGroupsView>()
            .AddSingleton<RenameView>()
            .AddSingleton<UpdateWarningView>()
            .AddSingleton<MainPresenter>()
            .AddSingleton<MonsterPresenter>()
            .AddSingleton<DropGroupPresenter>()
            .AddSingleton<FilterMonsterPresenter>()
            .AddSingleton<FilterDropGroupsPresenter>()
            .AddSingleton<SetItemPresenter>()
            .AddSingleton<SetDropGroupPresenter>()
            .AddSingleton<RenameDropGroupPresenter>()
            .AddSingleton<ConfigManager>()
            .AddSingleton<DBManager>()
            .AddSingleton<DropRepository>()
            .AddSingleton<MonsterRepository>()
            .AddSingleton<MonsterCache>()
            .AddSingleton<DropGroupCache>()
            .AddSingleton<ItemCache>();

        ServiceProvider = services.BuildServiceProvider();

        // instantiate Presenter that are not called elsewhere
        ServiceProvider.GetRequiredService<DropGroupPresenter>();
        ServiceProvider.GetRequiredService<MonsterPresenter>();
        ServiceProvider.GetRequiredService<MainPresenter>();
    }
}

// workflow (ablauf nutzerinteraktion), wireframes (balsamiq, figma), epics, komponentendiagramm, 
// schriftlicher vorschlag so sieht das ding aus, das kann das ding