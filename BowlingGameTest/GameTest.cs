namespace BowlingGame.Test;

public class GameTest
{
    private readonly Game game;

    public GameTest()
    {
        game = new Game("The Dude");
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
    public void RollInFinishedGameShouldThrow()
    {
        RollMany(20, 0);

        Assert.True(game.Status == GameStatus.Finished);
        var e = Assert.Throws<InvalidOperationException>(() => game.Roll(0));
        Assert.Equal("Game is over. You cannot roll any more.", e.Message);
    }

    [Fact]
    public void PlayerNameIsOk()
    {
        Assert.Equal("The Dude", game.PlayerName);
    }

    [Fact]
    public void GetScoreShouldReturnZeroBeforeFirstRoll()
    {
        Assert.Equal(0, game.GetScore());
    }

    [Fact]
    public void TestSingleSpare()
    {
        // Frame 1
        game.Roll(3);
        game.Roll(6);

        // Frame 2
        game.Roll(7);
        game.Roll(3);  // Spare

        // Frame 3
        game.Roll(6);
        game.Roll(1);

        Assert.Equal(4, game.CurrentFrameNo);
        Assert.True(game.GetFrame(2).IsSpare());
        Assert.Equal(16, game.GetFrame(2).Score);
        Assert.Equal(32, game.GetScore());
        Assert.True(game.Status == GameStatus.Ongoing);
    }

    [Fact]
    public void TestSingleSpareAndTwoPendingStrikeCalculations()
    {
        // Frame 1
        game.Roll(3);
        game.Roll(6);

        // Frame 2
        game.Roll(7);
        game.Roll(3);  // Spare

        // Frame 3
        game.Roll(10); // Strike

        // Frame 4
        game.Roll(10); // Strike

        Assert.Equal(5, game.CurrentFrameNo);
        Assert.True(game.GetFrame(2).IsSpare());
        Assert.Equal(20, game.GetFrame(2).Score);
        Assert.True(game.GetFrame(3).IsStrike());
        Assert.Null(game.GetFrame(3).Score);
        Assert.True(game.GetFrame(4).IsStrike());
        Assert.Null(game.GetFrame(4).Score);
        Assert.Equal(29, game.GetScore());
        Assert.True(game.Status == GameStatus.Ongoing);
    }

    [Fact]
    public void TestSingleSpareSingleStrikeAndPendingSpareCaculations()
    {
        // Frame 1
        game.Roll(3);
        game.Roll(6);

        // Frame 2
        game.Roll(7);
        game.Roll(3);  // Spare

        // Frame 3
        game.Roll(10); // Strike

        // Frame 4
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
        Assert.True(game.Status == GameStatus.Ongoing);
    }

    [Fact]
    public void TestSampleGame()
    {
        // Frame 1
        game.Roll(1);
        game.Roll(4);
        Assert.Equal(5, game.GetScore());

        // Frame 2
        game.Roll(4);
        game.Roll(5);
        Assert.Equal(14, game.GetScore());

        // Frame 3
        game.Roll(6); 
        game.Roll(4);  // Spare
        Assert.Equal(14, game.GetScore());

        // Frame 4
        game.Roll(5);  
        game.Roll(5);  // Spare
        Assert.Equal(29, game.GetScore());

        // Frame 5
        game.Roll(10); // Strike
        Assert.Equal(49, game.GetScore());

        // Frame 6
        game.Roll(0);
        game.Roll(1);
        Assert.Equal(61, game.GetScore());

        // Frame 7
        game.Roll(7);
        game.Roll(3);  // Spare
        Assert.Equal(61, game.GetScore());

        // Frame 8
        game.Roll(6);
        game.Roll(4);  // Spare
        Assert.Equal(77, game.GetScore());

        // Frame 9
        game.Roll(10); // Strike
        Assert.Equal(97, game.GetScore());

        // Frame 10
        game.Roll(2);
        game.Roll(8);  // Spare
        Assert.Equal(117, game.GetScore());

        // Extra roll
        game.Roll(6);
        Assert.Equal(133, game.GetScore());

        Assert.True(game.Status == GameStatus.Finished);
    }

    [Fact]
    public void TestGutterGame()
    {
        RollMany(20, 0);

        Assert.Equal(0, game.GetScore());
        Assert.True(game.Status == GameStatus.Finished);
    }

    [Fact]
    public void TestAllOnes()
    {
        RollMany(20, 1);

        Assert.Equal(20, game.GetScore());
        Assert.True(game.Status == GameStatus.Finished);
    }

    [Fact]
    public void TestPerfectGame()
    {
        RollMany(12, 10);

        Assert.Equal(300, game.GetScore());
        Assert.True(game.Status == GameStatus.Finished);
    }

    private void RollMany(int n, int pins)
    {
        for (int i = 0; i < n; i++)
        {
            game.Roll(pins);
        }
    }
}