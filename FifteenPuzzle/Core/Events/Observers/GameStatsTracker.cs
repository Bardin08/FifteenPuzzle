using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Events.Observers;

public class GameStatsTracker : IGameEventObserver
{
    private DateTime _gameStartedAt;
    private DateTime _gameCompletedAt;

    private int _totalMoves;

    public void OnGameEvent(IGameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case GameStartedEvent started:
                _totalMoves = 0;
                _gameStartedAt = started.Timestamp;
                break;

            case TileMovedEvent:
                _totalMoves++;
                break;

            case GameCompletedEvent completed:
                _gameCompletedAt = completed.Timestamp;
                break;
        }
    }

    public GameStats GetStatistics() => new(
        _totalMoves,
        _gameCompletedAt.Subtract(_gameStartedAt)
    );
}