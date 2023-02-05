namespace BowlingGame.Test
{
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
    }
}