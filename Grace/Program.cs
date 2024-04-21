using Grace.Cache;
using Grace.Config;
using Grace.Model.DataContext;
using Grace.Model.Repository;
using Grace.Presenter;
using Grace.View;
using Microsoft.Extensions.DependencyInjection;

namespace Grace;

// suppressing since ServiceProvider != null after constructor
// and GetService<MainView>() does not return null
#pragma warning disable CS8604, CS8618
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        // Configure services and instantiate MainPresenter
        ConfigureServices();
        GetService<MainPresenter>();

        Application.Run(GetService<MainView>());
    }

    public static IServiceProvider ServiceProvider { get; private set; }
    static void ConfigureServices()
    {
        var services = new ServiceCollection()
            .AddSingleton<MainView>()
            .AddSingleton<FilterView>()
            .AddSingleton<ConfigManager>()
            .AddSingleton<DBManager>()
            .AddSingleton<DropGroupRepository>()
            .AddSingleton<DropTableRepository>()
            .AddSingleton<MonsterRepository>()
            .AddSingleton<ItemCache>()
            .AddSingleton<MonsterCache>()
            .AddSingleton<FilterPresenter>()
            .AddSingleton<MainPresenter>();

        ServiceProvider = services.BuildServiceProvider();
    }

    public static T? GetService<T>() where T : class
    {
        return (T?)ServiceProvider.GetService(typeof(T));
    }
}
