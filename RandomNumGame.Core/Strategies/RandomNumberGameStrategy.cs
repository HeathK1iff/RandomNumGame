using RandomNumGame.Core.Abstractions;
using RandomNumGame.Core.Types;

namespace RandomNumGame.Core.Strategies
{
    public class RandomNumberGameStrategy : NumberGameStrategy
    {
        public static int DefaultDeltaRange = 30;

        protected override NumberRange CreateRange()
        {
            var randomizer = new Random();
            int startRange = randomizer.Next(10, 999);

            return new NumberRange()
            {
                StartRange = startRange,
                EndRange = startRange + DefaultDeltaRange
            };
        }
    }
}
