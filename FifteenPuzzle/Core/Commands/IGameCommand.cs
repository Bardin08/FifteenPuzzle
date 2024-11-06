namespace FifteenPuzzle.Core.Commands;

public interface IGameCommand
{
    public string Name { get; }
    public string Description { get; }
}