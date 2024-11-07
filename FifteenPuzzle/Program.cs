using FifteenPuzzle;
using FifteenPuzzle.Cli.Commands;
using FifteenPuzzle.Cli.Console;
using FifteenPuzzle.Core.Services;

var gameEngine = new GameEngine();
var commandProcessor = new CommandProcessor(gameEngine);
var renderer = new ConsoleRenderer();
var parser = new CommandParser(gameEngine, renderer);

var gameController = new GameController(
    gameEngine: gameEngine,
    commandParser: parser,
    uiRenderer: renderer,
    commandProcessor: commandProcessor);

gameController.Execute();