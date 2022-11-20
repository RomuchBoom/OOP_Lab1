using System;
namespace GameClasses;


public class GameAccount
{
    public string UserName { get; set; }
    public int BaseRating { get; set; }
    public int CurrentRating
    {
        get
        {
            int val = 0;
            foreach(var elem in allGameStats)
            {
                val += elem.GameRating;
            }
            return (val + BaseRating);
        }
    }
    public int GamesCount { get; set; }
    public List<GameStats> allGameStats = new List<GameStats>(); 

    public GameAccount(string UserName, int BaseRating, int GamesCount)
    {
        this.UserName = UserName;
        this.BaseRating = Math.Max(1, BaseRating);
        this.GamesCount = GamesCount;
    }

    public void WinGame(GameAccount opponent, int rating, int Counter = 2)
    {
        if(rating < 0) 
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "Game rating mustn't be negative");
        }
        if(Counter == 0) return;
        var currentGame = new GameStats(rating, opponent.UserName, "Win");
        allGameStats.Add(currentGame);
        this.GamesCount++;
        opponent.LoseGame(this, rating, --Counter);
    }
    

    public void LoseGame(GameAccount opponent, int rating, int Counter = 2)
    {
        if(rating < 0) 
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "Game rating mustn't be negative");
        }
        if(Counter == 0) return;
        var currentGame = new GameStats(-rating, opponent.UserName, "Lose");
        allGameStats.Add(currentGame);
        this.GamesCount++;
        opponent.WinGame(this, rating, --Counter);
    }

    public void GetStats()
    {
        Console.WriteLine($"\n--- {this.UserName}'s account game history:\n");
        Console.WriteLine("////////////////////");
        foreach(var elem in allGameStats)
        {
            Console.WriteLine($"\nOpponent Name: {elem.OpponentName}");
            Console.WriteLine($"Game Result: {elem.GameResult}");
            Console.WriteLine($"Game rating: {elem.GameRating}");
            Console.WriteLine($"Game ID: {elem.GameID}\n");
            Console.WriteLine("////////////////////");
        }
    }

}