namespace BowlingGame;

public class Game
{
    private int[] rolls = new int[21];

    public Game(string playerName)
    {
        if (string.IsNullOrEmpty(playerName)) 
        { 
            throw new ArgumentNullException(nameof(playerName));
        }
        PlayerName = playerName;

        Frames = new Frame[10];
    }

    public string PlayerName { get; }

    public int CurrentFrame { get; } = 1;

    public Frame[] Frames { get; }

    public int Score { get; }

    public void Roll(int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new ArgumentException("Illegal number of pins.", nameof(pins));
        }
    }
}