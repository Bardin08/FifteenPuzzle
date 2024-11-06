namespace FifteenPuzzle.Core.Commands;

public interface ICommandProcessor 
{
    bool Execute(IGameCommand command);
    bool CanExecute(IGameCommand command);
}