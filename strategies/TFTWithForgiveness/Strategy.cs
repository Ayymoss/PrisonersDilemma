using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace TFTWithForgiveness;

public class Strategy : IStrategy
{
    public string Name => "TFT with Forgiveness";

    public string Description => "This variant of TFT incorporates a forgiveness mechanism. After the opponent defects, TFT with " +
                                 "Forgiveness might defect for a set number of rounds but then revert to cooperation with some " +
                                 "probability. This allows for reconciliation after a period of retaliation without always holding " +
                                 "grudges forever.";

    public string Author { get; }
    public string Version { get; }

    private const double ForgivenessProbability = 0.1;

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        if (opponentHistory.Count is 0) return Task.FromResult(Choice.Cooperate);

        var opponentLastChoice = opponentHistory.Last();

        return opponentLastChoice == Choice.Cooperate
            ? Task.FromResult(Choice.Cooperate)
            : Task.FromResult(Random.Shared.NextDouble() < ForgivenessProbability ? Choice.Cooperate : Choice.Defect);
    }
}
