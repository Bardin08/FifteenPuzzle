using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Interfaces;

public interface IGameEngine
{
    bool IsRunning { get; }
    bool PuzzleSolved { get; }

    IReadOnlyList<Move> Moves { get; }

    void Initialize();
    bool MakeMove(Move move);
    void UndoLastMove();
    void Reset();
    IBoard GetCurrentBoard();
}