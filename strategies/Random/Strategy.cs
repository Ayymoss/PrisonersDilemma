using PrisonersDilemma.Abstraction.Enums;
using PrisonersDilemma.Abstraction.Interfaces;

namespace Random;

public class Strategy : IStrategy
{
    public string Name => "Random";

    public string Description => "This strategy simply chooses to cooperate or defect randomly, making its moves unpredictable and " +
                                 "potentially less exploitable than purely deterministic strategies. However, it might also miss out " +
                                 "on opportunities for long-term cooperation and mutual benefit.";

    public string Author => "Amos";
    public string Version => "2023-12-24";

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        var choice = System.Random.Shared.Next(2) == 0 ? Choice.Cooperate : Choice.Defect;
        return Task.FromResult(choice);
    }
}
