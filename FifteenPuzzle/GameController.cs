using FifteenPuzzle.Cli.Display;
using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle;

public class GameController(
    IGameEngine gameEngine,
    ICommandParser commandParser,
    IUiRenderer uiRenderer,
    ICommandProcessor commandProcessor)
{
    private readonly IGameEngine _gameEngine = gameEngine;
    private readonly ICommandProcessor _commandProcessor = commandProcessor;
    private readonly ICommandParser _commandParser = commandParser;
    private readonly IUiRenderer _uiRenderer = uiRenderer;

    public void Execute()
    {
        ShowWelcomeScreen();
        _uiRenderer.RenderInfo(Messages.Instructions);

        while (!_gameEngine.IsRunning)
        {
            ShowPrompt();
            var input = Console.ReadLine();

            HandleInput(input!);
        }

        GameLoop();
    }

    private void GameLoop()
    {
        while (!_gameEngine.IsSolved())
        {
            ShowPrompt();
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                _uiRenderer.RenderWarning("Invalid input.");
                continue;
            }

            HandleInput(input);
        }

        _uiRenderer.RenderInfo("Puzzle solved. Congratulations!");
    }


    private void ShowWelcomeScreen()
    {
        Console.Clear();
        _uiRenderer.RenderInfo(Messages.WelcomeScreen);

        Thread.Sleep(1500);
        Console.Clear();
    }

    private void ShowPrompt()
    {
        _uiRenderer.RenderInfo("\nEnter your move ➡️ ");
    }

    private void ShowVictoryScreen()
    {
        Console.Clear();
        _uiRenderer.RenderBoard(_gameEngine.GetCurrentBoard());
        _uiRenderer.RenderInfo(Messages.VictoryScreen);

        _uiRenderer.RenderInfo("\nPress any key to exit...");
        Console.ReadKey(true);
    }

    private void HandleInput(string input)
    {
        var command = _commandParser.Parse(input);
        if (command is null)
        {
            _uiRenderer.RenderError("Command can not be null");
            return;
        }

        command.Execute();

        if (_gameEngine.IsRunning)
           _uiRenderer.RenderBoard(_gameEngine.GetCurrentBoard());


        // if (!_commandProcessor.CanExecute(command))
        // {
        //     _uiRenderer.RenderError($"Cannot execute {command.Name} now");
        //     return;
        // }
        //
        // var completed = _commandProcessor.Execute(command);
        // if (completed)
        // {
        //     var currentBoard = _gameEngine.GetCurrentBoard();
        //     _uiRenderer.RenderBoard(currentBoard);
        // }
        // else
        // {
        //     _uiRenderer.RenderError("Error while executing command");
        // }
    }
}