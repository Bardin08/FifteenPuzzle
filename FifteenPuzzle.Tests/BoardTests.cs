using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Tests;

public class BoardTests
{
    private readonly Board _board = new();

    [Theory]
    [MemberData(nameof(ValidMoves))]
    public void CanMove_ToEmptyTile_ShouldReturnTrue(Move move)
    {
        var canMove = _board.CanMove(move);
        Assert.True(canMove);
    }

    [Theory]
    [MemberData(nameof(InvalidMoves))]
    public void CanMove_ToBusyTile_ShouldReturnFalse(Move move)
    {
        var canMove = _board.CanMove(move);
        Assert.False(canMove);
    }

    [Fact]
    public void MoveTile_ValidMove_ShouldSwapTiles()
    {
        var move = new Move
        {
            MoveDirection = Move.Direction.Left,
            TileNumber = 1
        };

        _board.MoveTile(move);
        
        Assert.Equal(_board.Grid[0], 1);        
        Assert.Equal(_board.Grid[1], -1);        
    }

    public static IEnumerable<object[]> ValidMoves => new List<object[]>
    {
        new object[] { new Move { MoveDirection = Move.Direction.Left, TileNumber = 1 } },
        new object[] { new Move { MoveDirection = Move.Direction.Up, TileNumber = 4 } },
        new object[] { new Move { MoveDirection = Move.Direction.Right, TileNumber = -1 } },
        new object[] { new Move { MoveDirection = Move.Direction.Down, TileNumber = -1 } },
    };

    public static IEnumerable<object[]> InvalidMoves => new List<object[]>
    {
        new object[] { new Move { MoveDirection = Move.Direction.Down, TileNumber = 3 } },
    };
}