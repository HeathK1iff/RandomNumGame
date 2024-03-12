using RandomNumGame.Core.Abstractions;
using RandomNumGame.Core.Entities;
using RandomNumGame.Core.Repository;

namespace RandomNumGame.Core.Services
{
    public class UserService : IUserService
    {
        public static string AdminUserName = "admin";

        private readonly IUsersScoreRepository _repository;

        public UserService(IUsersScoreRepository repository)
        {
            _repository = repository;
        }

        public void Post(User user)
        {
            _repository.Update(user);
        }

        public User GetUser(string userName)
        {
            if (userName.Equals(AdminUserName, StringComparison.InvariantCultureIgnoreCase))
            {
                return new Administrator();
            }

            return GetOrCreateUser(userName);
        }

        private User GetOrCreateUser(string userName)
        {
            User? user = _repository.GetAll().FirstOrDefault(f => f.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));

            if (user == null)
            {
                Guid id = _repository.Create(new Player()
                {
                    UserName = userName
                });

                return new Player()
                {
                    Id = id,
                    UserName = userName
                };
            }

            return user;
        }

    }
}
