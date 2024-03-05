using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomNumGame.Core;
using RandomNumGame.Core.Abstractions;
using RandomNumGame.Core.Entities;
using RandomNumGame.Core.Repository;
using RandomNumGame.Core.Services;
using RandomNumGame.Core.Strategies;
using RandomNumGame.Infrastructure.Implementations;

const string ConfigurationFileName = "App.ini";
const string ScoresFileName = "Scores.json";

IServiceCollection serviceDescriptors = new ServiceCollection();
IConfiguration configuration = new ConfigurationBuilder()
                                 .AddIniFile(ConfigurationFileName).Build();

serviceDescriptors.AddScoped<IConfiguration>(_ => configuration);
serviceDescriptors.AddTransient<IUsersScoreRepository>(_ => new UsersScoreRepository(ScoresFileName));
serviceDescriptors.AddTransient<INumberGameStrategy, PredefinedNumberGameStrategy>();
serviceDescriptors.AddTransient<IAskQuestionGameBuilder, NumberGameBuilder>();
serviceDescriptors.AddScoped<IStrategyCreator, StrategyCreator>();
serviceDescriptors.AddTransient<IUserService, UserService>();
serviceDescriptors.AddTransient<IGameService, GameService>();

using (var serviceProvider = serviceDescriptors.BuildServiceProvider())
{
    IGameService gameService = serviceProvider.GetRequiredService<IGameService>();
    IUserService userService = serviceProvider.GetRequiredService<IUserService>();

    Console.Write("Please input user name:");
    string userName = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrWhiteSpace(userName))
        throw new ArgumentOutOfRangeException("User name shoud be empty");


    User user = userService.GetUser(userName);

    user.OnAskQuestionEvent += (question) =>
    {
        Console.Write(question);
        return Console.ReadLine() ?? string.Empty;
    };

    user.OnNotificationEvent += (promt) =>
    {
        Console.Write(promt);
    };

    gameService.Play(user);
    userService.Post(user);

    System.Console.WriteLine("Press any key for exit");
    System.Console.ReadKey();
}

