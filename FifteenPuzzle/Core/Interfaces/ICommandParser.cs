namespace FifteenPuzzle.Core.Interfaces;

public interface ICommandParser
{
    IGameCommand? Parse(string input);
}
