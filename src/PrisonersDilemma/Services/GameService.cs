using PrisonersDilemma.Caches;
using PrisonersDilemma.Models;
using PrisonersDilemma.Strategy.Enums;
using PrisonersDilemma.Utilities;
using Spectre.Console;

namespace PrisonersDilemma.Services;

public class GameService(StrategiesCache strategiesCache, GameCache gameCache)
{
    public async Task StartGameAsync()
    {
        var strategies = strategiesCache.Strategies.ToList();
        foreach (var sOne in strategies)
        {
            gameCache.GamesMap.Add(sOne, new StrategyGames {Games = []});

            foreach (var sTwo in strategies)
            {
                if (sOne == sTwo) continue;
                AnsiConsole.MarkupLine($"Starting game between [green]{sOne.Name}[/] and [red]{sTwo.Name}[/]");

                // Clear any previous games constants
                await sOne.InitializeAsync();
                await sTwo.InitializeAsync();

                // Games
                for (var i = 0; i < 200; i++)
                {
                    var currentGame = new Game
                    {
                        GameState = GameState.InProgress,
                        Opponent = sTwo,
                        Points = 0,
                        Moves = [],
                        OpponentMoves = []
                    };

                    gameCache.GamesMap[sOne].Games.Add(currentGame);

                    // Rounds
                    for (var j = 0; j < 10; j++)
                    {
                        var oneResult = await sOne.TurnAsync(sTwo.Name, currentGame.Moves, currentGame.OpponentMoves);
                        currentGame.Moves.Add(oneResult);
                        var twoResult = await sTwo.TurnAsync(sOne.Name, currentGame.OpponentMoves, currentGame.Moves);
                        currentGame.OpponentMoves.Add(twoResult);

                        var points = Helpers.PointLookup(oneResult, twoResult);
                        currentGame.Points += points.OnePoints;
                        currentGame.OpponentPoints += points.TwoPoints;
                    }

                    var gameIndex = gameCache.GamesMap[sOne].Games.IndexOf(currentGame);
                    gameCache.GamesMap[sOne].Games[gameIndex].GameState = GameState.Finished;
                }
            }
        }
    }

    public void PostGameAnalysis()
    {
        OverviewResults();
        PerStrategyResults();
    }

    private void PerStrategyResults()
    {
        var topRule = new Rule("[yellow]Per Strategy Analysis[/]")
        {
            Style = Color.Blue
        };
        topRule.LeftJustified();
        AnsiConsole.Write(topRule);

        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow([
            new Text("Strategy", new Style(Color.Yellow, Color.Black)).LeftJustified(),
            new Text("Strategy Points Mean", new Style(Color.Blue, Color.Black)).LeftJustified(),
            new Text("Strategy Points Median", new Style(Color.Blue, Color.Black)).LeftJustified(),
            new Text("Strategy Points", new Style(Color.Blue, Color.Black)).LeftJustified(),
            new Text("Opponent", new Style(Color.Yellow, Color.Black)).LeftJustified(),
            new Text("Opponent Points", new Style(Color.Yellow, Color.Black)).LeftJustified(),
            new Text("Total Cooperates", new Style(Color.Green, Color.Black)).LeftJustified(),
            new Text("Total Defects", new Style(Color.Red, Color.Black)).LeftJustified(),
            new Text("Total Moves", new Style(Color.Aqua, Color.Black)).LeftJustified(),
            new Text("Total Games", new Style(Color.Grey, Color.Black)).LeftJustified()
        ]);

        var ordered = gameCache.GamesMap.OrderByDescending(x => x.Value.TotalPoints).ToList();
        foreach (var strategy in ordered)
        {
            var opponentGamesSummed = strategy.Value.Games.GroupBy(x => x.Opponent.Name).Select(x => new
            {
                Opponent = x.Key,
                Games = x.ToList(),
                TotalPoints = x.Sum(y => y.Points),
                MeanPoints = x.Average(y => y.Points),
                MedianPoints = Helpers.GetMedian(x.Select(y => (double)y.Points).ToArray()),
                OpponentPoints = x.Sum(y => y.OpponentPoints),
                TotalCooperates = x.Sum(y => y.Moves.Count(z => z is Choice.Cooperate)),
                TotalDefects = x.Sum(y => y.Moves.Count(z => z is Choice.Defect)),
                TotalMoves = x.Sum(y => y.Moves.Count),
                TotalGames = x.Count()
            }).OrderByDescending(x => x.TotalPoints).ToList();

            foreach (var opponent in opponentGamesSummed)
            {
                grid.AddRow([
                    new Text(strategy.Key.Name, new Style(Color.Green)).LeftJustified(),
                    new Text(opponent.MeanPoints.ToString("N1")).LeftJustified(),
                    new Text(opponent.MedianPoints.ToString("N1")).LeftJustified(),
                    new Text(opponent.TotalPoints.ToString("N0")).LeftJustified(),
                    new Text(opponent.Opponent, new Style(Color.Red)).LeftJustified(),
                    new Text(opponent.OpponentPoints.ToString("N0")).LeftJustified(),
                    new Text(opponent.TotalCooperates.ToString("N0")).LeftJustified(),
                    new Text(opponent.TotalDefects.ToString("N0")).LeftJustified(),
                    new Text(opponent.TotalMoves.ToString("N0")).LeftJustified(),
                    new Text(opponent.TotalGames.ToString("N0")).LeftJustified(),
                ]);
            }
        }

        AnsiConsole.Write(grid);
    }

    private void OverviewResults()
    {
        var topRule = new Rule("[yellow]Global Game Analysis[/]")
        {
            Style = Color.Blue
        };
        topRule.LeftJustified();
        AnsiConsole.Write(topRule);

        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow([
            new Text("Strategy", new Style(Color.Yellow, Color.Black)).LeftJustified(),
            new Text("Mean Points", new Style(Color.Blue, Color.Black)).LeftJustified(),
            new Text("Median Points", new Style(Color.Blue, Color.Black)).LeftJustified(),
            new Text("Total Points", new Style(Color.Blue, Color.Black)).LeftJustified(),
            new Text("Total Cooperates", new Style(Color.Green, Color.Black)).LeftJustified(),
            new Text("Total Defects", new Style(Color.Red, Color.Black)).LeftJustified(),
            new Text("Total Moves", new Style(Color.Aqua, Color.Black)).LeftJustified(),
            new Text("Total Games", new Style(Color.Grey, Color.Black)).LeftJustified()
        ]);

        var ordered = gameCache.GamesMap.OrderByDescending(x => x.Value.TotalPoints).ToList();
        foreach (var strategy in ordered)
        {
            var medianPoints = strategy.Value.Games.Select(x => x.Points).ToList();
            var median = Helpers.GetMedian(medianPoints.Select(x => (double)x).ToArray());

            grid.AddRow([
                new Text(strategy.Key.Name, new Style(Color.Green)).LeftJustified(),
                new Text(strategy.Value.Games.Average(x => x.Points).ToString("N1")).LeftJustified(),
                new Text(median.ToString("N1")).LeftJustified(),
                new Text(strategy.Value.TotalPoints.ToString("N0")).LeftJustified(),
                new Text(strategy.Value.TotalCooperates.ToString("N0")).LeftJustified(),
                new Text(strategy.Value.TotalDefects.ToString("N0")).LeftJustified(),
                new Text(strategy.Value.TotalMoves.ToString("N0")).LeftJustified(),
                new Text(strategy.Value.Games.Count.ToString("N0")).LeftJustified(),
            ]);
        }

        AnsiConsole.Write(grid);
    }
}
