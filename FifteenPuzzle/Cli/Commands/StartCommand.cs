using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Cli.Commands;

public record StartCommand : IGameCommand
{
    private readonly IGameEngine _engine;

    public StartCommand(IGameEngine engine) => _engine = engine;

    public string Name => "start";
    public string Description => "Start a new game";

    public bool Execute()
    {
        _engine.Initialize();
        return true;
    }
}