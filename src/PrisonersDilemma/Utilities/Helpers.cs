using PrisonersDilemma.Abstraction.Enums;

namespace PrisonersDilemma.Utilities;

public abstract class Helpers
{
    public static (int OnePoints, int TwoPoints) PointLookup(Choice playerOne, Choice playerTwo) => (playerOne, playerTwo) switch
    {
        (Choice.Cooperate, Choice.Cooperate) => (3, 3),
        (Choice.Cooperate, Choice.Defect) => (0, 5),
        (Choice.Defect, Choice.Cooperate) => (5, 0),
        (Choice.Defect, Choice.Defect) => (1, 1),
        _ => throw new ArgumentException("Invalid result combination")
    };

    public static double GetMedian(double[] sourceNumbers)
    {
        if (sourceNumbers == null || sourceNumbers.Length == 0) throw new Exception("Median of empty array not defined.");

        var sortedPNumbers = (double[])sourceNumbers.Clone();
        Array.Sort(sortedPNumbers);

        var size = sortedPNumbers.Length;
        var mid = size / 2;
        var median = size % 2 != 0 ? sortedPNumbers[mid] : (sortedPNumbers[mid] + sortedPNumbers[mid - 1]) / 2;
        return median;
    }
}
