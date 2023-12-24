using PrisonersDilemma.Strategy.Enums;

namespace PrisonersDilemma.Models;

public class StrategyGames
{
    public List<Game> Games { get; set; }

    public int TotalPoints => Games.Sum(x => x.Points);
    public int TotalDefects => Games.Sum(x => x.Moves.Count(y => y is Choice.Defect));
    public int TotalCooperates => Games.Sum(x => x.Moves.Count(y => y is Choice.Cooperate));
    public int TotalMoves => Games.Sum(x => x.Moves.Count);
}
