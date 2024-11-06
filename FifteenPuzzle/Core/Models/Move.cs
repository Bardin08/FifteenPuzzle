namespace FifteenPuzzle.Core.Models;

public record Move
{
    public Direction MoveDirection { get; init; }
    public int TileNumber { get; init; }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}