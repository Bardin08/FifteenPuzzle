namespace FifteenPuzzle.Core.Interfaces;

public interface IObservable
{
    void AddObserver(IGameEventObserver observer);
    void RemoveObserver(IGameEventObserver observer);
    void Notify(IGameEvent gameEvent);
}