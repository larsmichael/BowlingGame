namespace BowlingGame;

public class Game
{
#warning remove?
    private int[] rolls = new int[21];
    private int currentRoll = 0;

    public Game(string playerName)
    {
        if (string.IsNullOrEmpty(playerName)) 
        { 
            throw new ArgumentNullException(nameof(playerName));
        }
        PlayerName = playerName;

        Frames = new Frame[10];
        for (int i = 0; i < 10; i++)
        {
            Frames[i] = new Frame(i);
        }
    }

    public string PlayerName { get; }

    public int CurrentFrameIndex { get; set; } = 0;

    public Frame[] Frames { get; }

    public int GetScore()
    {
        var score = 0;
        for (int i = 0; i < CurrentFrameIndex; i++)
        {
            score += Frames[i].Score is null ? 0 : (int)Frames[i].Score!;
        }
        return score;  
    }

    public void Roll(int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new ArgumentException("Illegal number of pins.", nameof(pins));
        }

        rolls[currentRoll++] = pins;

        var frame = Frames[CurrentFrameIndex];

        // Last frame
        if (CurrentFrameIndex == 9)
        {
            // TODO
        }

        if (frame.Roll1 is null)
        {
            frame.Roll1 = pins;
        }
        else
        {
            frame.Roll2 = pins;
        }

        CalculateMissingFrameScores();

        if (frame.IsStrike() || frame.Roll2 is not null)
        {
            if (!frame.IsSpare())
            {
                frame.Score = frame.Roll1 + frame.Roll2;
            }

            CurrentFrameIndex += 1;
        }
    }

    private void CalculateMissingFrameScores()
    {
        for (int i = 0; i < CurrentFrameIndex; i++)
        {
            var frame = Frames[i];
            if (frame.Score is null)
            {
                if (frame.IsSpare())
                {
                    frame.Score = CalculateSpare(i);
                }
                else if (frame.IsStrike())
                {
                    frame.Score = TryCalculateStrike(i);
                }
            }
        }
    }

    private int? TryCalculateStrike(int frameIndex)
    {
        var result = 10 + Frames[frameIndex + 1].Roll1;
        if (Frames[frameIndex + 1].Roll2 is not null)
        {
            result += Frames[frameIndex + 1].Roll2;
        }
        else if (Frames[frameIndex + 2].Roll1 is not null)
        {
            result += Frames[frameIndex + 2].Roll1;
        }
        else
        {
            return null;
        }
        return result;
    }

    private int? CalculateSpare(int frameIndex)
    {
        return 10 + Frames[frameIndex +1].Roll1;
    }
}