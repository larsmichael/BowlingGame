namespace BowlingGame
{
    public class Game
    {
        public Game(string playerName)
        {
            if (string.IsNullOrEmpty(playerName)) 
            { 
                throw new ArgumentNullException(nameof(playerName));
            }
            PlayerName = playerName;
        }

        public string PlayerName { get; }

        public void Roll(int pins)
        {
            if (pins < 0 || pins > 10)
            {
                throw new ArgumentException("illegal number of pins.", nameof(pins));
            }
        }
    }
}