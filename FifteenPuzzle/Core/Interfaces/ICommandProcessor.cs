namespace FifteenPuzzle.Core.Interfaces;

public interface ICommandProcessor 
{
    bool Execute(IGameCommand command);
    bool CanExecute(IGameCommand command);
}