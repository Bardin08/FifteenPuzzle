using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Interfaces;

public interface IBoard
{
    List<int> Grid { get; set; }
    bool CanMove(Move move);
    void MoveTile(Move move);
}
