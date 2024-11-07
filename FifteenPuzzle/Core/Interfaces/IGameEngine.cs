using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Interfaces;

public interface IGameEngine
{
    bool IsRunning { get; }

    void Initialize();
    bool MakeMove(Move move);
    bool IsSolved();
    void Reset();
    IBoard GetCurrentBoard();
}