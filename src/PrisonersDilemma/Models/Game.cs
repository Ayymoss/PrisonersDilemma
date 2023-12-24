using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Strategy.Interfaces;

namespace PrisonersDilemma.Models;

public class Game
{
    public GameState GameState { get; set; }
    public List<Choice> Moves { get; set; } = [];
    public IStrategy Opponent { get; set; }
    public List<Choice> OpponentMoves { get; set; } = [];
    public int Points { get; set; }
    public int OpponentPoints { get; set; }
}
