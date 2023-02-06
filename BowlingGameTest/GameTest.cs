using System.Runtime.CompilerServices;

namespace BowlingGame.Test;

public class GameTest
{
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
        var game = new Game("John Doe");
        var e = Assert.Throws<ArgumentException>(() => game.Roll(pins));
        Assert.Contains("Illegal number of pins.", e.Message);
    }

    [Fact]
    public void GetScoreShouldReturnZeroBeforeFirstRoll()
    {
        var game = new Game("John Doe");
        Assert.Equal(0, game.GetScore());
    }

    [Fact]
    public void Preliminary()
    {
        var game = new Game("John Doe");
        game.Roll(3);
        game.Roll(6);
        game.Roll(7);
        game.Roll(3);  // spare
        game.Roll(6);
        game.Roll(1);
        Assert.Equal(32, game.GetScore());
    }

    [Fact]
    public void Preliminary2()
    {
        var game = new Game("John Doe");
        game.Roll(3);
        game.Roll(6);
        game.Roll(7);
        game.Roll(3);  // spare
        game.Roll(10); // strike
        game.Roll(10); // strike
        Assert.Equal(29, game.GetScore());
    }

    [Fact]
    public void Preliminary3()
    {
        var game = new Game("John Doe");
        game.Roll(3);
        game.Roll(6);
        game.Roll(7);
        game.Roll(3);  // spare
        game.Roll(10); // strike
        game.Roll(5);
        game.Roll(5);  // spare
        Assert.Equal(49, game.GetScore());
    }



}