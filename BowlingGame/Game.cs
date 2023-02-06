namespace BowlingGame;

public class Game
{
    private int _frameIndex = 0;
    private readonly Frame[] _frames = new Frame[10];

    public Game(string playerName)
    {
        if (string.IsNullOrEmpty(playerName)) 
        { 
            throw new ArgumentNullException(nameof(playerName));
        }
        PlayerName = playerName;

        for (int i = 0; i < 10; i++)
        {
            _frames[i] = new Frame(i);
        }
    }

    public string PlayerName { get; }

    public int CurrentFrameNo => _frameIndex + 1;

    public Frame GetFrame(int frameNo)
    {
        if (frameNo < 0 || frameNo > 10)
        {
            throw new ArgumentException($"Non existing frame number {frameNo}.", nameof(frameNo));
        }
        return _frames[frameNo - 1];
    }

    public int GetScore()
    {
        var score = 0;
        for (int i = 0; i < _frameIndex; i++)
        {
            score += _frames[i].Score is null ? 0 : (int)_frames[i].Score!;
        }
        return score;  
    }

    public void Roll(int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new ArgumentException("Illegal number of pins.", nameof(pins));
        }

        var frame = _frames[_frameIndex];

        // Last frame
        if (CurrentFrameNo == 10)
        {
            // TODO
        }

        if (frame.First is null)
        {
            frame.First = pins;
        }
        else
        {
            frame.Second = pins;
        }

        CalculateMissingFrameScores();

        if (frame.IsStrike() || frame.Second is not null)
        {
            if (!frame.IsSpare())
            {
                frame.Score = frame.First + frame.Second;
            }

            _frameIndex += 1;
        }
    }

    private void CalculateMissingFrameScores()
    {
        for (int i = 0; i < _frameIndex; i++)
        {
            var frame = _frames[i];
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
        var result = 10 + _frames[frameIndex + 1].First;
        if (_frames[frameIndex + 1].Second is not null)
        {
            result += _frames[frameIndex + 1].Second;
        }
        else if (_frames[frameIndex + 2].First is not null)
        {
            result += _frames[frameIndex + 2].First;
        }
        else
        {
            return null;
        }
        return result;
    }

    private int? CalculateSpare(int frameIndex)
    {
        return 10 + _frames[frameIndex +1].First;
    }
}