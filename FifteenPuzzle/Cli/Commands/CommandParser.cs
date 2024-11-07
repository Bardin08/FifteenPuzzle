using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Cli.Commands;

public class CommandParser : ICommandParser
{
    private readonly IGameEngine _engine;
    private readonly IUiRenderer _renderer;
    private readonly Dictionary<string, IGameCommand> _commands;

    public CommandParser(IGameEngine engine, IUiRenderer renderer)
    {
        _engine = engine;
        _renderer = renderer;
        _commands = InitializeCommands();
    }

    public IReadOnlyCollection<IGameCommand> AvailableCommands => _commands.Values;

    public IGameCommand Parse(string input)
    {
        var loweredInput = input.ToLower().Trim();

        if (loweredInput.StartsWith("move "))
        {
            var direction = loweredInput[5..].Trim().ToLower() switch
            {
                "up" or "u" => Move.Direction.Up,
                "down" or "d" => Move.Direction.Down,
                "left" or "l" => Move.Direction.Left,
                "right" or "r" => Move.Direction.Right,
                _ => throw new ArgumentException("Invalid direction")
            };
            return new MoveTileCommand(_engine, direction);
        }

        if (_commands.TryGetValue(loweredInput, out var command))
        {
            return command;
        }

        throw new ArgumentException("Unknown command");
    }

    private Dictionary<string, IGameCommand> InitializeCommands()
    {
        return new Dictionary<string, IGameCommand>
        {
            ["start"] = new StartCommand(_engine),
            ["restart"] = new RestartCommand(_engine),
            ["quit"] = new QuitCommand(),
            ["show"] = new ShowCommand(_engine, _renderer),
            ["help"] = new HelpCommand(_renderer),
        };
    }
}
