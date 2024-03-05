namespace RandomNumGame.Infrastructure.Dto
{
    internal class UserScoreDto
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int SuccessScore { get; set; }
        public int FailScore { get; set; }

    }
}
