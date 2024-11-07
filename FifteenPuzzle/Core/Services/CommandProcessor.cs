using FifteenPuzzle.Cli.Commands;
using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Services;

public class CommandProcessor(IGameEngine gameEngine) : ICommandProcessor
{
    private readonly IGameEngine _gameEngine = gameEngine;

    private static readonly HashSet<string> GeneralCommands =
    [
        nameof(HelpCommand),
        nameof(QuitCommand),
    ];
    
    private static readonly HashSet<string> GameLoopCommands = 
    [
        nameof(RestartCommand),
        nameof(ShowCommand),
        nameof(MoveTileCommand)
    ];


    public bool Execute(IGameCommand command)
    {
        // TODO: track analytics, log command exec, and so..
        return command.Execute();
    }

    public bool CanExecute(IGameCommand command)
    {
        var commandName = command.GetType().Name;

        if (GeneralCommands.Contains(commandName))
            return true;

        if (_gameEngine.IsRunning)
        {
            if (GameLoopCommands.Contains(commandName))
                return true;
        }
        else
        {
            if (commandName is nameof(StartCommand))
                return true;
        }

        return false;
    }
}