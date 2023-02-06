namespace BowlingGame;

public class Frame
{
    private readonly int _index;

    public Frame(int index) => _index = index;

    public int? First { get; internal set; }

    public int? Second { get; internal set; }

    public int? Last { get; internal set; }

    public int? Score { get; internal set; }

    public bool IsStrike()
    {
        return First == 10;
    }

    public bool IsSpare()
    {
        return First + Second == 10;
    }

    public override string ToString()
    {
        return $"Frame {_index + 1}";
    }
}