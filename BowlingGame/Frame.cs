namespace BowlingGame;

public class Frame
{
    private readonly int _index;

    public Frame(int index)
    { 
        _index = index;
    }

    public int? Roll1 { get; set; }

    public int? Roll2 { get; set; }

    public int? Roll3 { get; set; }

    public bool IsStrike()
    {
        return Roll1 == 10;
    }
    
    public bool IsSpare()
    {
        return Roll1 + Roll2 == 10;
    }

    public int? Score { get; set; }

    public override string ToString()
    {
        return $"Frame {_index + 1}";
    }
}
