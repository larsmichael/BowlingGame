namespace BowlingGame.Test
{
    public class GameTest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void RollWithIllegalNumberOfPinsShouldThrow(int pins)
        {
            var game = new Game("John Doe");
            var e = Assert.Throws<ArgumentException>(() => game.Roll(pins));
        }
    }
}