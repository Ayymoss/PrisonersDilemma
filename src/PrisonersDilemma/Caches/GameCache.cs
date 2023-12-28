using System.Collections.Concurrent;
using PrisonersDilemma.Abstraction.Interfaces;
using PrisonersDilemma.Models;

namespace PrisonersDilemma.Caches;

public class GameCache
{
    public ConcurrentDictionary<IStrategy, StrategyGames> GamesMap { get; set; } = [];
}
