using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Cli.Commands;

public record ShowCommand : IGameCommand
{
    private readonly IGameEngine _engine;
    private readonly IUiRenderer _renderer;

    public ShowCommand(IGameEngine engine, IUiRenderer renderer)
    {
        _engine = engine;
        _renderer = renderer;
    }

    public string Name => "show";
    public string Description => "Display current puzzle state";

    public bool Execute()
    {
        _renderer.RenderBoard(_engine.GetCurrentBoard());
        return true;
    }
}