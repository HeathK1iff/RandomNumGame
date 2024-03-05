using RandomNumGame.Core.Entities;

namespace RandomNumGame.Core.Abstractions
{
    public interface IAskQuestionGameBuilder
    {
        void StartGameRound(User user);
        void PrintWelcome(User user);
        void PrintGameRules(User user);
        string AskQuestion(User user);
        public bool IsContinueAsk(bool success);
        bool CheckAnswer(string value, out string clueMessage);
    }   
}
