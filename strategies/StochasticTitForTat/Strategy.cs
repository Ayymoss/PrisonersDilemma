using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace StochasticTitForTat;

public class Strategy : IStrategy
{
    public string Name => "Stochastic Tit for Tat";

    public string Description => "Similar to TFT, STFT cooperates on the first round and then mimics the opponent's previous move with " +
                                 "a high probability (e.g., 90%). However, there's a small chance (e.g., 10%) of defecting even when " +
                                 "the opponent cooperated, introducing an element of unpredictability and reducing the risk of exploitation.";

    public string Author { get; }
    public string Version { get; }

    private const double DeviationProbability = 0.1;

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        if (opponentHistory.Count is 0) return Task.FromResult(Choice.Cooperate);

        var opponentLastChoice = opponentHistory.Last();

        return Random.Shared.NextDouble() < DeviationProbability
            ? Task.FromResult(Random.Shared.NextDouble() < 0.5 ? Choice.Cooperate : Choice.Defect)
            : Task.FromResult(opponentLastChoice);
    }
}
