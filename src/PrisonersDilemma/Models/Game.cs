using PrisonersDilemma.Abstraction.Enums;
using PrisonersDilemma.Abstraction.Interfaces;
using PrisonersDilemma.Enums;

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
