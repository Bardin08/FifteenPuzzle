using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Commands;

public class UndoCommand(IGameEngine gameEngine) : IGameCommand
{
    private readonly IGameEngine _gameEngine = gameEngine;

    public string Name => "undo";
    public string Description => "Rollback the current state";

    public bool Execute()
    {
        _gameEngine.UndoLastMove();
        return true;
    }
}