using PrisonersDilemma.Caches;
using PrisonersDilemma.Services;

namespace PrisonersDilemma;

public class Main(BuildStrategies buildStrategies, GameService gameService, StrategiesCache strategiesCache)
{
    public async Task RunAsync()
    {
        var strategies = buildStrategies.SetupStrategies().ToList();
        strategiesCache.RegisterStrategies(strategies);
        await gameService.StartGameAsync();
        gameService.PostGameAnalysis();
    }
}
