namespace BowlingGame
{
    public class LastFrame : Frame
    {
        public LastFrame() : base(9)
        {
        }

        public int? Last { get; private set; }

        public override void Roll(int pins)
        {
            if (First is null)
            {
                First = pins;
            }
            else if (Second is null)
            {
                Second = pins;
            }
            else
            {
                Last = pins;
            }

            if (IsStrike() && Second is { } && Last is { })
            {
                Score = First + Second + Last;
                OnCompleted();
            }

            if (IsSpare() && Last is { })
            {
                Score = 10 + Last;
                OnCompleted();
            }

            if (First is { } && Second is { } && !IsStrike() && !IsSpare())
            {
                Score = First + Second;
                OnCompleted();
            }
        }
    }
}
