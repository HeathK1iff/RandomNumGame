using RandomNumGame.Core.Entities;

namespace RandomNumGame.Core.Repository
{
    public interface IUsersScoreRepository
    {
        Guid Create(User user);
        void Update(User user);
        User[] GetAll();
    }
}
