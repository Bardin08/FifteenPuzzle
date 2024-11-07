using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Interfaces;

public interface IBoard
{
    List<int> Grid { get; }
    bool CanMove(Move move);
    void MoveTile(Move move);
    void Shuffle(int seed = 12345, int n = 10_000);
}
