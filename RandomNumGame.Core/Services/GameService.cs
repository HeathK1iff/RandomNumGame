using Microsoft.Extensions.Configuration;
using RandomNumGame.Core.Abstractions;
using RandomNumGame.Core.Entities;

namespace RandomNumGame.Core.Services
{
    public class GameService : IGameService
    {
        private IAskQuestionGameBuilder _builder;
        private int _statTotalGames;
        private int _statSuccessGames;

        public GameService(IAskQuestionGameBuilder builder)
        {
            _builder = builder;
        }

        public void Play(User user)
        {
            _builder.PrintWelcome(user);
            PrintPlayerScore(user);
            _builder.PrintGameRules(user);

            ResetGameStatistic();
            do
            {
                bool success = false;
                StartGameRound(user);
                do
                {
                    string answer;
                    try
                    {
                        answer = AskQuestion(user);
                        
                        success = _builder.CheckAnswer(answer, out string clueMessage);
                        if (success)
                        {
                            break;
                        }

                        NotifyClue(user, clueMessage);
                    }
                    catch (FormatException ex)
                    {
                        user.Notify(ex.Message + '\n');
                    }
                    catch
                    {
                        throw;
                    }

                } while (_builder.IsContinueAsk(success));


                PrintGameResult(user, success);
            } while (AskRepeatStartGame(user));

            PrintTotalStatistic(user);
        }

        private void PrintPlayerScore(User user)
        {
            if (user is IScorable scorable)
            {
                user.Notify($"Success: {scorable.GetSuccessScore()}; Fail: {scorable.GetFailScore()}\n");
            }
        }

        private void ResetGameStatistic()
        {
            _statTotalGames = 0;
            _statSuccessGames = 0;
        }

        private void StartGameRound(User user)
        {
            _builder.StartGameRound(user);
        }

        private void NotifyClue(User user, string clueMessage)
        {
            user.Notify($"Clue: {clueMessage} \n");
        }

        private string AskQuestion(User user)
        {
            return _builder.AskQuestion(user);
        }

        private void PrintTotalStatistic(User user)
        {
            int failureGames = _statTotalGames - _statSuccessGames;
            user.Notify($"Total: {_statTotalGames}; Success: {_statSuccessGames}; Failure: {failureGames}\n");
        }

        private void PrintGameResult(User user, bool successGame)
        {
            if (user is INotifable notify)
            {
                IScorable? scorable = GetScorable(user);

                if (successGame)
                {
                    notify.Notify("You are winner!\n");
                    scorable?.IncreaseSuccessScoreCounter();
                    _statSuccessGames++;
                    return;
                }

                scorable?.IncreaseFailScoreCounter();
                notify.Notify("You are looser!\n");
            }
        }

        private IScorable? GetScorable(User user)
        {
            if (user is IScorable scorable)
            {
                return scorable;
            }

            return null;
        }

        private bool AskRepeatStartGame(User user)
        {
            _statTotalGames++;
            if (user is IAskable notify)
            {
                string answer = notify.Ask("Do you want to repeat game (y/n)?:");
                return answer.Equals("y", StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
    }
}
