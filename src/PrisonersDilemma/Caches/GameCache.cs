using PrisonersDilemma.Models;
using PrisonersDilemma.Strategy.Interfaces;

namespace PrisonersDilemma.Caches;

public class GameCache
{
    public Dictionary<IStrategy, StrategyGames> GamesMap { get; set; } = [];
}
