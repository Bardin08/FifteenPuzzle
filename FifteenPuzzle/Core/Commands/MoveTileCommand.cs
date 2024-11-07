using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Commands;

public record MoveTileCommand : IGameCommand
{
    private const int EmptyTileNumber = -1;

    private readonly IGameEngine _engine;
    private readonly Move.Direction _direction;

    public MoveTileCommand(IGameEngine engine, Move.Direction direction)
    {
        _engine = engine;
        _direction = direction;
    }

    public string Name => "move <direction>";
    public string Description => "Move empty tile";

    public bool Execute() => _engine.MakeMove(new Move
    {
        MoveDirection = _direction,
        TileNumber = EmptyTileNumber
    });
}