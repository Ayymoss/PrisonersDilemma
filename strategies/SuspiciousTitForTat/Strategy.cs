using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace SuspiciousTitForTat;

public class Strategy : IStrategy
{
    public string Name => "Suspicious Tit for Tat";
    public string Description => "Starts with defection and then mimics the opponent's last move.";
    public string Author => "Amos";
    public string Version => "2023-12-24";

    private Choice _lastOpponentChoice;

    public Task InitializeAsync()
    {
        _lastOpponentChoice = Choice.Defect;
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        if (opponentHistory.Count is not 0) _lastOpponentChoice = opponentHistory.Last();
        return Task.FromResult(_lastOpponentChoice);
    }
}
