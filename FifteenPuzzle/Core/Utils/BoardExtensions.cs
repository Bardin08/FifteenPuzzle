using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Utils;

public static class BoardExtensions
{
    public static void Shuffle(this IBoard board, int seed = 12345, int n = 10_000)
    {
        var random = new Random(seed);

        var movesLeft = 10_000;
        while (movesLeft > 0)
        {
            var direction = (Move.Direction)random.Next(0, 4);

            var move = new Move
            {
                TileNumber = -1,
                MoveDirection = direction
            };

            if (!board.CanMove(move))
            {
                continue;
            }

            board.MoveTile(move);
            movesLeft--;
        }
    }
}