using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Infrastructure.Persistence;
using FifteenPuzzle.Infrastructure.Persistence.Models;

namespace FifteenPuzzle.Core.Events.Observers;

public class EventsFlowObserver(IFileSystemRepository repository) : IGameEventObserver
{
    private readonly IFileSystemRepository _repository = repository;

    public void OnGameEvent(IGameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case GameStartedEvent started:
                _repository.Initialize(started.InitBoard.Grid);
                break;

            case GameRestartedEvent:
                _repository.Reset();
                break;
            
            case TileMovedEvent moved:
                _repository.AddMove(new MoveDescriptor
                {
                    TileNumber = moved.Move.TileNumber,
                    Direction = (int)moved.Move.MoveDirection,
                    Timestamp = new DateTimeOffset(moved.Timestamp).ToUnixTimeMilliseconds()
                });
                break;

            case GameCompletedEvent completed:
                _repository.GameComplete(completed.Timestamp);
                _repository.Save();
                break;
        }
    }
}