using FifteenPuzzle;
using FifteenPuzzle.Cli.Commands;
using FifteenPuzzle.Cli.Display;
using FifteenPuzzle.Core.Services;

var gameEngine = new GameEngine();
var parser = new CommandParser();
var commandProcessor = new CommandProcessor();
var renderer = new ConsoleRenderer();

var gameController = new GameController(
    gameEngine: gameEngine,
    commandParser: parser,
    uiRenderer: renderer,
    commandProcessor: commandProcessor);

gameController.Execute();