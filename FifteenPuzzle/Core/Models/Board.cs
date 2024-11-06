using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Models;

public class Board : IBoard
{
    public List<int> Grid { get; set; }

    public Board()
    {
        var tiles = Enumerable.Range(0, 16)
            .Select(x => x)
            .ToArray();

        tiles[0] = -1;
        Grid = tiles.ToList();
    }

    public bool CanMove(Move move)
    {
        const int emptyCellNumber = -1;
        
        var currentIndex = GetTileIndex(move.TileNumber);
        if (currentIndex == -1)
            return false;

        var targetIndex = GetTargetIndex(move, currentIndex);

        var indexInGridBound = move.MoveDirection switch
        {
            Move.Direction.Up => currentIndex < 4, // First row
            Move.Direction.Down => currentIndex >= 12, // Last row
            Move.Direction.Left => currentIndex % 4 == 0, // First column
            Move.Direction.Right => currentIndex % 4 == 3, // Last column
            _ => true
        };

        if (indexInGridBound)
            return false;

        return Grid[targetIndex] == emptyCellNumber ||
               Grid[currentIndex] == emptyCellNumber;
    }

    private static int GetTargetIndex(Move move, int currentIndex)
    {
        return move.MoveDirection switch
        {
            Move.Direction.Up => currentIndex - 4,
            Move.Direction.Down => currentIndex + 4,
            Move.Direction.Left => currentIndex - 1,
            Move.Direction.Right => currentIndex + 1,
            _ => throw new ArgumentException("Invalid direction")
        };
    }

    public void MoveTile(Move move)
    {
        var currentIndex = GetTileIndex(move.TileNumber);
        var targetIndex = GetTargetIndex(move, currentIndex);

        (Grid[currentIndex], Grid[targetIndex]) = (Grid[targetIndex], Grid[currentIndex]);
    }

    private int GetTileIndex(int tileNumber) => Grid.IndexOf(tileNumber);
}