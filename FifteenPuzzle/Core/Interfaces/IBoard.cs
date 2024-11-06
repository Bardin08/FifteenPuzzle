using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Interfaces;

public interface IBoard
{
    int[,] Grid { get; }
    Tile EmptyTile { get; }
    bool CanMove(Move move);
    void MoveTile(Move move);
    Tile GetTilePosition(int tileNumber);
}
