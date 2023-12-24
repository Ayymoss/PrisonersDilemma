using PrisonersDilemma.Caches;
using PrisonersDilemma.Services;

namespace PrisonersDilemma;

public class Main(StrategySetup strategySetup, GameService gameService, StrategiesCache strategiesCache)
{
    public async Task RunAsync()
    {
        var strategies = strategySetup.SetupStrategies().ToList();
        strategiesCache.RegisterStrategies(strategies);
        await gameService.StartGameAsync();
        gameService.PostGameAnalysis();
    }
}
