namespace BowlingGame.Test;

public class FrameTest
{
    [Fact]
    public void ToStringIsOk()
    {
        var frame = new Frame(9);
        Assert.Equal("Frame 10", frame.ToString());
    }

    [Fact]
    public void IsStrikeIsOk()
    {
        var frame = new Frame(9)
        {
            First = 10
        };
        Assert.True(frame.IsStrike());
        Assert.False(frame.IsSpare());
    }

    [Fact]
    public void IsSpareIsOk()
    {
        var frame = new Frame(9)
        {
            First = 9,
            Second = 1
        };
        Assert.False(frame.IsStrike());
        Assert.True(frame.IsSpare());
    }
}
