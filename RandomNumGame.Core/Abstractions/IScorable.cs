namespace RandomNumGame.Core.Abstractions
{
    public interface IScorable
    {
        int GetSuccessScore();
        int GetFailScore();
        
        void IncreaseSuccessScoreCounter();
        void IncreaseFailScoreCounter();
    }
}
