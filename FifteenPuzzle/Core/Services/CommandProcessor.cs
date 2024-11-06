using FifteenPuzzle.Core.Commands;

namespace FifteenPuzzle.Core.Services;

public class CommandProcessor : ICommandProcessor
{
    public bool Execute(IGameCommand command)
    {
        throw new NotImplementedException();
    }

    public bool CanExecute(IGameCommand command)
    {
        throw new NotImplementedException();
    }
}