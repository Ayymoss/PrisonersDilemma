using PrisonersDilemma.Abstraction.Interfaces;
using PrisonersDilemma.Models;

namespace PrisonersDilemma.Caches;

public class GameCache
{
    public Dictionary<IStrategy, StrategyGames> GamesMap { get; set; } = [];
}
