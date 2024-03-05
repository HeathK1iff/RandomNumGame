using RandomNumGame.Core.Entities;

namespace RandomNumGame.Core.Abstractions
{
    public interface IUserService
    {
        User GetUser(string userName);
        void Post(User user);
    }
}
