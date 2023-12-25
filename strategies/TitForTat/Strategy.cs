using PrisonersDilemma.Abstraction.Enums;
using PrisonersDilemma.Abstraction.Interfaces;

namespace TitForTat;

public class Strategy : IStrategy
{
    public string Name => "Tit For Tat";

    public string Description => "This popular strategy starts by cooperating and then mimics the opponent's previous move in each " +
                                 "subsequent round. It fosters cooperation when met with cooperation but punishes defection with " +
                                 "defection, promoting tit-for-tat reciprocity. TFT was famously successful in Robert Axelrod's " +
                                 "repeated Prisoner's Dilemma tournaments. ";

    public string Author => "Amos";
    public string Version => "2023-12-24";

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory)
    {
        var choice = opponentHistory.LastOrDefault();
        return Task.FromResult(choice);
    }
}
