using Microsoft.Extensions.DependencyInjection;
using PrisonersDilemma.Caches;
using PrisonersDilemma.Services;

namespace PrisonersDilemma;

public class Setup
{
    public static async Task Main(string[] args)
    {
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
