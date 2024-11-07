using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;
using FifteenPuzzle.Core.Utils;

namespace FifteenPuzzle.Core.Services;

public class GameEngine : IGameEngine
{
    private IBoard _board = null!;

    public bool IsRunning { get; private set;  } = false;

    public void Initialize()
    {
        _board = new Board();
        _board.Grid.Shuffle();

        IsRunning = true;
    }

    public void Reset() => Initialize();

    public IBoard GetCurrentBoard() => _board!;

    public bool IsSolved()
    {
        var numbers = _board.Grid.ToList();
        return numbers.SequenceEqual(numbers.OrderBy(x => x));
    }

    public bool MakeMove(Move move)
    {
        var canMove = _board.CanMove(move);
        if (!canMove)
            return false;

        _board.MoveTile(move);
     
        
        return true;
    }
}