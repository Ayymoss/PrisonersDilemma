using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace TitForTwoTats;

public class Strategy : IStrategy
{
    public string Name => "Tit for Two Tats";
    public string Description => "Cooperates by default; defects only after two consecutive defections by the opponent.";
    public string Author => "Amos";
    public string Version => "2023-12-24";

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        if (opponentHistory.Count < 2) return Task.FromResult(Choice.Cooperate);
        var lastDefection = opponentHistory.Skip(opponentHistory.Count - 2).Take(2).All(c => c == Choice.Defect);
        return Task.FromResult(lastDefection ? Choice.Defect : Choice.Cooperate);
    }
}
