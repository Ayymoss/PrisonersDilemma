using PrisonersDilemma.Abstraction.Enums;

namespace PrisonersDilemma.Abstraction.Interfaces;

public interface IStrategy
{
    public string Name { get; }
    public string Description { get; }
    public string Author { get; }
    public string Version { get; }

    Task InitializeAsync();

    Task<Choice> TurnAsync(string opponentName, IReadOnlyCollection<Choice> history, IReadOnlyCollection<Choice> opponentHistory);
}
