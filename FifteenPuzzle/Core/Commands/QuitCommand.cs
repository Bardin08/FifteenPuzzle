using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Commands;

public record QuitCommand : IGameCommand
{
    public string Name => "quit";
    public string Description => "Exit the game";

    public bool Execute()
    {
        Environment.Exit(0);
        return true;
    }
}