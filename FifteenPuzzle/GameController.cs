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

    public bool IsRunning => !_gameEngine.IsSolved();

    public void Execute()
    {
        while (!_gameEngine.IsSolved())
        {
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

    private void HandleInput(string input)
    {
        var command = _commandParser.Parse(input);
        if (command is null)
        {
            _uiRenderer.RenderError("Command can not be null");
            return;
        }

        if (!_commandProcessor.CanExecute(command))
        {
            _uiRenderer.RenderError($"Cannot execute {command.Name} now");
            return;
        }

        var completed = _commandProcessor.Execute(command);
        if (completed)
        {
            var currentBoard = _gameEngine.GetCurrentBoard();
            _uiRenderer.RenderBoard(currentBoard);
        }
        else
        {
            _uiRenderer.RenderError("Error while executing command");
        }
    }
}