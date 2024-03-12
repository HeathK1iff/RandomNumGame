using Microsoft.Extensions.Configuration;
using RandomNumGame.Core.Abstractions;
using RandomNumGame.Core.Entities;
using RandomNumGame.Core.Types;
using System.Text.RegularExpressions;

namespace RandomNumGame.Core
{
    public class NumberGameBuilder : IAskQuestionGameBuilder
    {
        public static string RoundAttempKey = "roundAttemp";
        public static int DefaultRoundAttempt = 3;

        private readonly INumberGameStrategy _gameStrategy;
        private readonly int _attemptCount;
        private int _attempt;

        public NumberGameBuilder(IConfiguration configuration, IStrategyCreator strategyCreator)
        {
            _gameStrategy = strategyCreator.Create();
            _attemptCount = GetAttemptCountFromConfiguration(configuration);
        }

        private int GetAttemptCountFromConfiguration(IConfiguration configuration)
        {
            string? value = configuration[RoundAttempKey];

            if ((!string.IsNullOrEmpty(value)) && (int.TryParse(value, out int number)))
            {
                return number;
            }

            return DefaultRoundAttempt;
        }

        public string AskQuestion(User user)
        {
            string answer = user.Ask("Answer:");
            _attempt++;

            if (Regex.IsMatch(answer, @"\d+"))
            {    
                return answer;
            }

            throw new FormatException("Invalid format of answer.");
        }

        public bool CheckAnswer(string value, out string clueMessage)
        {
            NumberEquality result = _gameStrategy.Validate(int.Parse(value));
            clueMessage = GetClueMessage(result);

            if (result == NumberEquality.Equal)
            {
                return true;
            }
            return false;
        }

        private string GetClueMessage(NumberEquality result)
        {
            switch (result)
            {
                case NumberEquality.Less:
                    return "Input value is less";
                case NumberEquality.Greater:
                    return "Input value is greater ";
                default:
                    return string.Empty;
            }
        }

        public void PrintGameRules(User user)
        {
            user.Notify($"Dear user, you have {_attemptCount} attemption for Win this game.\n");
            user.Notify($"You need to guess a number from range of numbers\n");
        }

        public void PrintWelcome(User user)
        {
            user.Notify("Welcome. OTUS Random Number game.\n");
        }

        public void StartGameRound(User user)
        {
            NumberRange range = _gameStrategy.Generate();
            user.Notify($"Range numbers are from {range.StartRange} to {range.EndRange}\n");
            _attempt = 0;
        }


        public bool IsContinueAsk(bool success)
        {
            return (!success) && (_attempt < _attemptCount);
        }
    }
}