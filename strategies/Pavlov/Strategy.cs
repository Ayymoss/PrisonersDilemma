using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace Pavlov;

public class Strategy : IStrategy
{
    public string Name => "Pavlov";

    public string Description => "Inspired by the famous conditioning experiment, Pavlov cooperates after the opponent cooperates but " +
                                 "defects for a set number of rounds after the opponent defects. This strategy punishes defection but " +
                                 "allows for potential reconciliation after a period of retaliation.";

    public string Author => "Pavlov";
    public string Version => "2023-12-24";

    private Choice _lastChoice;

    public Task InitializeAsync()
    {
        _lastChoice = Choice.Cooperate;
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        if (history.Count <= 0) return Task.FromResult(_lastChoice);

        var lastRound = history.Last();
        var opponentLastRound = opponentHistory.Last();

        if (lastRound == opponentLastRound)
        {
            return Task.FromResult(_lastChoice);
        }

        _lastChoice = _lastChoice == Choice.Cooperate ? Choice.Defect : Choice.Cooperate;
        return Task.FromResult(_lastChoice);
    }
}
