using RandomNumGame.Core.Abstractions;

namespace RandomNumGame.Core.Entities
{
    public class Player: User, IScorable
    {
        private readonly int _success;
        private readonly int _fail;

        public Player(int success, int fail): this()
        {
            _success = success;
            _fail = fail;
        }

        public Player()
        {

        }

        public int GetSuccessScore()
        {
            return _success;
        }

        public int GetFailScore()
        {
            return _fail;
        }

        public void IncreaseSuccessScoreCounter()
        {
            _success++;
        }

        public void IncreaseFailScoreCounter()
        {
            _fail++;
        }
    }
}
