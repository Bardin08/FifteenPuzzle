using FifteenPuzzle;
using FifteenPuzzle.Cli.Commands;
using FifteenPuzzle.Cli.Console;
using FifteenPuzzle.Core.Events.Observers;
using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;
using FifteenPuzzle.Core.Services;
using FifteenPuzzle.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IGameEngine, GameEngine>();

services.AddSingleton<ICommandProcessor, CommandProcessor>();
services.AddSingleton<IUiRenderer, ConsoleRenderer>();
services.AddSingleton<ICommandParser, CommandParser>();

services.AddSingleton<EventsFlowObserver>();
services.AddSingleton<GameStatsTracker>();

services.AddSingleton<KeyboardInterceptor>();

services.AddSingleton<IFileSystemRepository, FileSystemRepository>();

services.AddSingleton<GameController>();

using var serviceProvider = services.BuildServiceProvider();

var gameController = serviceProvider.GetRequiredService<GameController>();
gameController.Execute();