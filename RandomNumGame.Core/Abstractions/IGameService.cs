using RandomNumGame.Core.Entities;

namespace RandomNumGame.Core.Abstractions
{
    public interface IGameService
    {
        void Play(User user);
    }
}
