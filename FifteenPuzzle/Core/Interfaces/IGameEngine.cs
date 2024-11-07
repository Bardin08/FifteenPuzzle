using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Interfaces;

public interface IGameEngine
{
    bool IsRunning { get; }
    bool PuzzleSolved { get; }

    void Initialize();
    bool MakeMove(Move move);
    void Reset();
    IBoard GetCurrentBoard();
}