using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;
using FifteenPuzzle.Core.Utils;

namespace FifteenPuzzle.Core.Services;

public class GameEngine : IGameEngine
{
    private IBoard _board = null!;

    public bool IsRunning { get; private set;  }
    public bool PuzzleSolved { get; private set; }

    public void Initialize()
    {
        _board = new Board();
        _board.Grid.Shuffle();

        IsRunning = true;
        PuzzleSolved = false;
    }

    public void Reset() => Initialize();

    public IBoard GetCurrentBoard() => _board;

    public bool MakeMove(Move move)
    {
        var canMove = _board.CanMove(move);
        if (!canMove)
            return false;

        _board.MoveTile(move);

        CheckIfSolved();
        return true;
    }

    private void CheckIfSolved()
    {
        var numbers = _board.Grid.ToList();
        PuzzleSolved = numbers.SequenceEqual(numbers.OrderBy(x => x));

        if (PuzzleSolved)
            IsRunning = false;
    }
}