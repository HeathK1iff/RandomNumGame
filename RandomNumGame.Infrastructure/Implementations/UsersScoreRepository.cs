using RandomNumGame.Core.Abstractions;
using RandomNumGame.Core.Entities;
using RandomNumGame.Core.Repository;
using RandomNumGame.Infrastructure.Dto;
using System.Text.Json;

namespace RandomNumGame.Infrastructure.Implementations
{
    public class UsersScoreRepository : IUsersScoreRepository
    {
        private readonly string _filePath;
        public UsersScoreRepository(string filePath) 
        {
            _filePath = filePath;
        }

        public Guid Create(User user)
        {
            UserScoreDto[] elements = LoadFromFile();
            var list = new List<UserScoreDto>(elements);
             
            Guid guid = Guid.NewGuid();
            list.Add(new UserScoreDto()
            {
                Id = guid.ToString(),
                UserName = user.UserName,
            });

            SaveToFile(list.ToArray());
            return guid;
        }

        public User[] GetAll()
        {
            UserScoreDto[] elements = LoadFromFile();

            return elements.Select(f => new Player(f.SuccessScore, f.FailScore)
            {
                Id = Guid.Parse(f.Id),
                UserName = f.UserName,
                
            }).ToArray<User>();
        }

        public void Update(User user)
        {
            UserScoreDto[] elements = LoadFromFile();
            UserScoreDto? element = elements.FirstOrDefault(f => f.Id.Equals(user.Id.ToString()));

            if (element != null)
            {
                element.UserName = user.UserName;
                
                if (user is IScorable scorable)
                {
                    element.SuccessScore = scorable.GetSuccessScore();
                    element.FailScore = scorable.GetFailScore();
                }

                SaveToFile(elements);
            }
        }

        private UserScoreDto[] LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new UserScoreDto[0];
            }
            
            var jsonText = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<UserScoreDto[]>(jsonText) ?? new UserScoreDto[0];
        }

        private void SaveToFile(UserScoreDto[] users)
        {
            string json = JsonSerializer.Serialize(users);
            File.WriteAllText(_filePath, json);
        }
    }
}
