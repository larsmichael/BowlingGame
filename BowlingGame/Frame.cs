using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BowlingGame.Test")]
namespace BowlingGame;

public class Frame
{
    public Frame(int index) => Index = index;

    public int? First { get; internal set; }

    public int? Second { get; internal set; }

    public int? Last { get; internal set; }

    public int? Score { get; internal set; }

    public int Index { get; }

    public bool IsStrike() => First == 10;

    public bool IsSpare() => First + Second == 10;

    public override string ToString() => $"Frame {Index + 1}";
}