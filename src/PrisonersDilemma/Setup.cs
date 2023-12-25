using Microsoft.Extensions.DependencyInjection;
using PrisonersDilemma.Caches;
using PrisonersDilemma.Services;

namespace PrisonersDilemma;

public static class Setup
{
    public static async Task Main()
    {
        if (!Directory.Exists(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "_Strategies")))
            Directory.CreateDirectory(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "_Strategies"));

        var serviceCollection = new ServiceCollection()
            .AddSingleton<StrategySetup>()
            .AddSingleton<GameService>()
            .AddSingleton<StrategiesCache>()
            .AddSingleton<GameCache>()
            .AddSingleton<Main>();

        await serviceCollection
            .BuildServiceProvider()
            .GetRequiredService<Main>().RunAsync();
    }
}
