using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle;

public class GameController(IGameEngine engine, ICommandParser parser, IGameRenderer renderer, ICommandProcessor commandProcessor)
{
    private readonly IGameEngine _engine = engine;
    private readonly ICommandProcessor _commandProcessor = commandProcessor;
    private readonly ICommandParser _parser = parser;
    private readonly IGameRenderer _renderer = renderer;

    public void HandleInput(string input)
    {
        var command = _parser.Parse(input);
        if (command is null)
        {
            _renderer.RenderError("Command can not be null");
            return;
        }

        if (!_commandProcessor.CanExecute(command))
        {
            _renderer.RenderError($"Cannot execute {command.Name} now");
            return;
        }

        var completed = _commandProcessor.Execute(command);
        if (completed)
        {
            var currentBoard = _engine.GetCurrentBoard();
            _renderer.RenderBoard(currentBoard);
        }
        else
        {
            _renderer.RenderError("Error while executing command");
        }
    }
}