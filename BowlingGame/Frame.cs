namespace BowlingGame;

public class Frame
{
    private readonly int _index;

    public Frame(int index) => _index = index;

    public int? First { get; set; }

    public int? Second { get; set; }

    public int? Last { get; set; }

    public int? Score { get; set; }

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
