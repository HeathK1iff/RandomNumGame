using RandomNumGame.Core.Abstractions;
using RandomNumGame.Core.Types;
using System;


namespace RandomNumGame.Core.Strategies
{
    public abstract class NumberGameStrategy : INumberGameStrategy
    {
        private int _generatedNumber;
        public NumberRange Generate()
        {
            NumberRange range = CreateRange();

            _generatedNumber = GenerateNumber(range);

            return range;
        }

        protected abstract NumberRange CreateRange();

        private int GenerateNumber(NumberRange range)
        {
            var randomizer = new Random();
            return randomizer.Next(range.StartRange, range.EndRange);
        }

        public NumberEquality Validate(int number)
        {
            if (number == _generatedNumber)
            {
                return NumberEquality.Equal;
            }
            else
           if (number < _generatedNumber)
            {
                return NumberEquality.Less;
            }
            return NumberEquality.Greater;
        }
    }
}
