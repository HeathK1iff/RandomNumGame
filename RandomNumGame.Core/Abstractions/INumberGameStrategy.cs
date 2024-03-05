using RandomNumGame.Core.Types;

namespace RandomNumGame.Core.Abstractions
{
    public interface INumberGameStrategy
    {
        NumberRange Generate();
        NumberEquality Validate(int number);
    };
}
