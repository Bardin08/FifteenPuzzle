using FifteenPuzzle.Core.Commands;

namespace FifteenPuzzle.Cli.Commands;

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