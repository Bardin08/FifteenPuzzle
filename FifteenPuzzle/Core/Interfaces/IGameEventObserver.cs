namespace FifteenPuzzle.Core.Interfaces;

public interface IGameEventObserver
{
    void OnGameEvent(IGameEvent gameEvent);
}