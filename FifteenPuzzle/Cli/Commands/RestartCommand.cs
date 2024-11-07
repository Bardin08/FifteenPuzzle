using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Cli.Commands;

public record RestartCommand : IGameCommand
{
    private readonly IGameEngine _engine;

    public RestartCommand(IGameEngine engine) => _engine = engine;

    public string Name => "restart";
    public string Description => "Reset current puzzle to initial state";

    public bool Execute()
    {
        _engine.Reset();
        return true;
    }
}