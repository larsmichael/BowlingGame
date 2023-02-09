namespace BowlingGame;

public class Frame
{
    public Frame(int index) => Index = index;

    public event EventHandler? Completed;

    public bool IsCompleted { get; private set; } = false;

    public virtual void Roll(int pins)
    {
        if (First is null)
        {
            First = pins;
            if (IsStrike())
            {
                OnCompleted();
            }
        }
        else
        {
            Second = pins;
            if (!IsSpare())
            {
                Score = First + Second;
            }
            OnCompleted();
        }
    }

    public int? First { get; protected set; }

    public int? Second { get; protected set; }

    public int? Score { get; internal set; }

    public int Index { get; }

    public bool IsStrike() => First == 10;

    public bool IsSpare() => First + Second == 10;

    public override string ToString() => $"Frame {Index + 1}";

    protected virtual void OnCompleted()
    {
        IsCompleted = true;
        Completed?.Invoke(this, new EventArgs());
    }
}