using FifteenPuzzle.Core.Commands;

namespace FifteenPuzzle.Core.Interfaces;

public interface ICommandParser
{
    IGameCommand? Parse(string input);
    IReadOnlyCollection<IGameCommand> AvailableCommands { get; }
}
