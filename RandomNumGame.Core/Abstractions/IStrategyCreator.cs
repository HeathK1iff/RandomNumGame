namespace RandomNumGame.Core.Abstractions
{
    public interface IStrategyCreator
    {
        INumberGameStrategy Create();
    }
}
