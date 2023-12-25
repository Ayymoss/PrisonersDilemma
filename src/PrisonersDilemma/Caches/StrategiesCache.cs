using PrisonersDilemma.Abstraction.Interfaces;

namespace PrisonersDilemma.Caches;

public class StrategiesCache
{
    private readonly List<IStrategy> _strategies = [];

    public IStrategy? GetStrategy(string name) => _strategies.FirstOrDefault(x => x.Name == name);
    public IEnumerable<IStrategy> Strategies => _strategies.AsReadOnly();
    public void RegisterStrategies(IEnumerable<IStrategy> strategies) => _strategies.AddRange(strategies);
}
