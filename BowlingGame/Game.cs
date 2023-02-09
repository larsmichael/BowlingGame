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

        for (int i = 0; i < 9; i++)
        {
            var frame = new Frame(i);
            frame.Completed += (object? _, EventArgs _) => _frameIndex += 1;
            _frames[i] = frame;
        }

        var lastFrame = new LastFrame();
        lastFrame.Completed += (object? _, EventArgs _) => Status = GameStatus.Finished;
        _frames[9] = lastFrame;
    }

    public GameStatus Status { get; private set; } = GameStatus.Ongoing;

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
        for (int i = 0; i <= _frameIndex; i++)
        {
            score += _frames[i].Score is null ? 0 : (int)_frames[i].Score!;
        }
        return score;
    }

    public void Roll(int pins)
    {
        if (Status == GameStatus.Finished)
        {
            throw new InvalidOperationException("Game is over. You cannot roll any more.");
        }

        if (pins < 0 || pins > 10)
        {
            throw new ArgumentException("Illegal number of pins.", nameof(pins));
        }

        var frame = _frames[_frameIndex];
        //if (CurrentFrameNo == 10)
        //{
        //    HandleLastFrame(frame, pins);
        //    return;
        //}
        
        frame.Roll(pins);
        
        CalculateMissingFrameScores();
    }

    private void HandleLastFrame(Frame frame, int pins)
    {
        //if (frame.First is null)
        //{
        //    frame.First = pins;
        //}
        //else if (frame.Second is null)
        //{
        //    frame.Second = pins;
        //}
        //else
        //{
        //    frame.Last = pins;
        //}

        //CalculateMissingFrameScores();

        //if (frame.IsStrike() && frame.Second is { } && frame.Last is { })
        //{
        //    frame.Score = frame.First + frame.Second + frame.Last;
        //    Status = GameStatus.Finished;
        //}

        //if (frame.IsSpare() && frame.Last is { })
        //{
        //    frame.Score = 10 + frame.Last;
        //    Status = GameStatus.Finished;
        //}

        //if (frame.First is { } && frame.Second is { } && !frame.IsStrike() && !frame.IsSpare())
        //{
        //    frame.Score = frame.First + frame.Second;
        //    Status = GameStatus.Finished;
        //}
    }

    private void CalculateMissingFrameScores()
    {
        foreach (var frame in _frames.Where(f => f.IsCompleted && f.Score is null))
        {
            if (frame.IsSpare())
            {
                frame.Score = TryCalculateSpare(frame.Index);
            }
            else if (frame.IsStrike())
            {
                frame.Score = TryCalculateStrike(frame.Index);
            }
        }
    }

    private int? TryCalculateStrike(int frameIndex)
    {
        var result = 10 + _frames[frameIndex + 1].First;
        if (_frames[frameIndex + 1].Second is { })
        {
            result += _frames[frameIndex + 1].Second;
        }
        else if (frameIndex != 8 && _frames[frameIndex + 2].First is { })
        {
            result += _frames[frameIndex + 2].First;
        }
        else
        {
            return null;
        }
        return result;
    }

    private int? TryCalculateSpare(int frameIndex)
    {
        if (_frames[frameIndex + 1].First is { })
        {
            return 10 + _frames[frameIndex + 1].First;
        }
        else
        {
            return null;
        }
    }
}