namespace FifteenPuzzle.Core.Interfaces;

public interface IGameCommand
{
    public string Name { get; }
    public string Description { get; }
    
    bool Execute();
}