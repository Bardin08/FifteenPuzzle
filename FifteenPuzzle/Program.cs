using FifteenPuzzle;
using FifteenPuzzle.Cli.Commands;
using FifteenPuzzle.Cli.Console;
using FifteenPuzzle.Core.Events.Observers;
using FifteenPuzzle.Core.Services;
using FifteenPuzzle.Infrastructure.Persistence;

var gameEngine = new GameEngine();

var repository = new FileSystemRepository();
var eventsFlowObserver = new EventsFlowObserver(repository);

gameEngine.AddObserver(eventsFlowObserver);

var commandProcessor = new CommandProcessor(gameEngine);
var renderer = new ConsoleRenderer();
var parser = new CommandParser(gameEngine, renderer);

var gameController = new GameController(
    gameEngine: gameEngine,
    commandParser: parser,
    uiRenderer: renderer,
    commandProcessor: commandProcessor);

gameController.Execute();