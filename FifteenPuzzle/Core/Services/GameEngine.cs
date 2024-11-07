using DeepCopy;
using FifteenPuzzle.Core.Events;
using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;
using FifteenPuzzle.Core.Utils;

namespace FifteenPuzzle.Core.Services;

public class GameEngine : IGameEngine
{
    private readonly List<IGameEventObserver> _eventObservers = [];
    private readonly Stack<Move> _moves = new();
    private IBoard _board = null!;
    private int _boardSeed;

    public bool IsRunning { get; private set; }
    public bool PuzzleSolved { get; private set; }

    public IReadOnlyList<Move> Moves => _moves.ToList();

    public void Initialize(bool regenerateSeed = true)
    {
        if (regenerateSeed)
            _boardSeed = DateTime.Now.Millisecond * DateTime.Now.Second + DateTime.Now.Minute;

        _board = new Board();
        _board.Grid.Shuffle(_boardSeed);

        IsRunning = true;
        CheckIfSolved();

        Notify(new GameStartedEvent(DeepCopier.Copy(_board), DateTime.Now));
    }

    public void Reset() => Initialize(regenerateSeed: false);

    public IBoard GetCurrentBoard() => _board;

    public bool MakeMove(Move move)
    {
        var canMove = _board.CanMove(move);
        if (!canMove)
            return false;

        _board.MoveTile(move);
        _moves.Push(move);

        Notify(new TileMovedEvent(move, DateTime.Now));

        CheckIfSolved();
        return true;
    }

    public void UndoLastMove()
    {
        if (_moves.Count == 0)
            return;

        var lastMove = _moves.Peek();

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

        if (!PuzzleSolved)
            return;

        IsRunning = false;
        Notify(new GameCompletedEvent(DateTime.Now));
    }

    public void AddObserver(IGameEventObserver observer) => _eventObservers.Add(observer);
    public void RemoveObserver(IGameEventObserver observer) => _eventObservers.Remove(observer);
    public void Notify(IGameEvent gameEvent) => _eventObservers.ForEach(observer => observer.OnGameEvent(gameEvent));
}