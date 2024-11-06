using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Models;

public class Board : IBoard
{
    private readonly int[,] _grid;
    public int[,] Grid => _grid;
    public required Tile EmptyTile { get; set; }

    public bool CanMove(Move move)
    {
        throw new NotImplementedException();
    }

    public void MoveTile(Move move)
    {
        throw new NotImplementedException();
    }

    public Tile GetTilePosition(int tileNumber)
    {
        throw new NotImplementedException();
    }
}