using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace AlwaysDefect;

public class Strategy : IStrategy
{
    public string Name => "Always Defect";

    public string Description => "This strategy always defects, maximizing self-gain regardless of the potential consequences for the " +
                                 "overall cooperation. While it might secure immediate benefits, it often leads to a tit-for-tat dynamic " +
                                 "that ultimately reduces overall gains for both players.";

    public string Author => "Amos";
    public string Version => "2023-12-24";

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        return Task.FromResult(Choice.Defect);
    }
}
