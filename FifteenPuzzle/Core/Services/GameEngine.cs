using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;
using FifteenPuzzle.Core.Utils;

namespace FifteenPuzzle.Core.Services;

public class GameEngine : IGameEngine
{
    private readonly Stack<Move> _moves = new();
    private IBoard _board = null!;

    public bool IsRunning { get; private set; }
    public bool PuzzleSolved { get; private set; }

    public IReadOnlyList<Move> Moves => _moves.ToList();

    public void Initialize()
    {
        _board = new Board();
        _board.Grid.Shuffle();

        IsRunning = true;
        CheckIfSolved();
    }

    public void Reset() => Initialize();

    public IBoard GetCurrentBoard() => _board;

    public bool MakeMove(Move move)
    {
        var canMove = _board.CanMove(move);
        if (!canMove)
            return false;

        _board.MoveTile(move);
        _moves.Push(move);

        CheckIfSolved();
        return true;
    }

    public void UndoLastMove()
    {
        if (_moves.Count == 0)
            return;

        var lastMove = _moves.Pop();

        var oppositeDirection = lastMove.MoveDirection switch
        {
            Move.Direction.Up => Move.Direction.Down,
            Move.Direction.Down => Move.Direction.Up,
            Move.Direction.Left => Move.Direction.Right,
            Move.Direction.Right => Move.Direction.Left,
            _ => throw new ArgumentOutOfRangeException()
        };

        MakeMove(lastMove with { MoveDirection = oppositeDirection });
    }

    private void CheckIfSolved()
    {
        var numbers = _board.Grid.ToList();
        PuzzleSolved = numbers.SequenceEqual(numbers.OrderBy(x => x));

        if (PuzzleSolved)
            IsRunning = false;
    }
}