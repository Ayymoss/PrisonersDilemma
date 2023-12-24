using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace GenerousTitForTat;

public class Strategy : IStrategy
{
    public string Name => "Generous Tit for Tat";
    public string Description => "Mostly cooperates, occasionally forgiving an opponent's defection.";
    public string Author => "Amos";
    public string Version => "2023-12-24";

    private const double ForgivenessFactor = 0.3;

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        if (opponentHistory.Count <= 0 || opponentHistory.Last() != Choice.Defect) return Task.FromResult(Choice.Cooperate);
        return Task.FromResult(Random.Shared.NextDouble() < ForgivenessFactor ? Choice.Cooperate : Choice.Defect);
    }
}
