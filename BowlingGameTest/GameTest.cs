namespace BowlingGame.Test;

public class GameTest
{
    private readonly Game game;

    public GameTest()
    {
        game = new Game("John Doe");
    }

    [Fact]
    public void CreateWithNullOrEmptyPlayerNameShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() => new Game(null!));
        Assert.Throws<ArgumentNullException>(() => new Game(""));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void RollWithIllegalNumberOfPinsShouldThrow(int pins)
    {
        var e = Assert.Throws<ArgumentException>(() => game.Roll(pins));
        Assert.Contains("Illegal number of pins.", e.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void GetFrameWithIllegalNumberShouldThrow(int frameNo)
    {
        var e = Assert.Throws<ArgumentException>(() => game.GetFrame(frameNo));
        Assert.Contains($"Non existing frame number {frameNo}.", e.Message);
    }

    [Fact]
    public void PlayerNameIsOk()
    {
        Assert.Equal("John Doe", game.PlayerName);
    }

    [Fact]
    public void GetScoreShouldReturnZeroBeforeFirstRoll()
    {
        Assert.Equal(0, game.GetScore());
    }

    [Fact]
    public void TestSingleSpare()
    {
        game.Roll(3);
        game.Roll(6);
        game.Roll(7);
        game.Roll(3);  // Spare
        game.Roll(6);
        game.Roll(1);

        Assert.Equal(4, game.CurrentFrameNo);
        Assert.True(game.GetFrame(2).IsSpare());
        Assert.Equal(16, game.GetFrame(2).Score);
        Assert.Equal(32, game.GetScore());
    }

    [Fact]
    public void TestSingleSpareAndTwoPendingStrikeCalculations()
    {
        game.Roll(3);
        game.Roll(6);
        game.Roll(7);
        game.Roll(3);  // Spare
        game.Roll(10); // Strike
        game.Roll(10); // Strike

        Assert.Equal(5, game.CurrentFrameNo);
        Assert.True(game.GetFrame(2).IsSpare());
        Assert.Equal(20, game.GetFrame(2).Score);
        Assert.True(game.GetFrame(3).IsStrike());
        Assert.Null(game.GetFrame(3).Score);
        Assert.True(game.GetFrame(4).IsStrike());
        Assert.Null(game.GetFrame(4).Score);
        Assert.Equal(29, game.GetScore());
    }

    [Fact]
    public void TestSingleSpareSingleStrikeAndPendingSpareCaculations()
    {
        game.Roll(3);
        game.Roll(6);
        game.Roll(7);
        game.Roll(3);  // Spare
        game.Roll(10); // Strike
        game.Roll(5);
        game.Roll(5);  // Spare

        Assert.Equal(5, game.CurrentFrameNo);
        Assert.True(game.GetFrame(2).IsSpare());
        Assert.Equal(20, game.GetFrame(2).Score);
        Assert.True(game.GetFrame(3).IsStrike());
        Assert.Equal(20, game.GetFrame(3).Score);
        Assert.True(game.GetFrame(4).IsSpare());
        Assert.Null(game.GetFrame(4).Score);
        Assert.Equal(49, game.GetScore());
    }

    [Fact]
    public void TestGutterGame()
    {
        RollMany(20, 0);
        Assert.Equal(0, game.GetScore());
    }

    [Fact]
    public void TestAllOnes()
    {
        RollMany(20, 1);
        Assert.Equal(20, game.GetScore());
    }

    private void RollMany(int n, int pins)
    {
        for (int i = 0; i < n; i++)
        {
            game.Roll(pins);
        }
    }




}