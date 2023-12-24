using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace AlwaysCooperate;

public class Strategy : IStrategy
{
    public string Name => "Always Cooperate";

    public string Description => "This strategy, as the name suggests, always cooperates regardless of the opponent's moves. " +
                                 "It prioritizes maintaining cooperation and building trust, even if it might be exploited. " +
                                 "While seemingly naive, it can foster cooperation in certain situations, especially against other " +
                                 "cooperative strategies.";

    public string Author => "Amos";
    public string Version => "2023-12-24";

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        return Task.FromResult(Choice.Cooperate);
    }
}
