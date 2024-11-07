using FifteenPuzzle.Cli.Console;
using FifteenPuzzle.Core.Events.Observers;
using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle;

public class GameController(
    IGameEngine gameEngine,
    ICommandProcessor commandProcessor,
    ICommandParser commandParser,
    IUiRenderer uiRenderer,
    KeyboardInterceptor keyboardInterceptor,
    GameStatsTracker gameStatsTracker,
    EventsFlowObserver eventsFlowObserver)
{
    private readonly IGameEngine _gameEngine = gameEngine;
    private readonly ICommandProcessor _commandProcessor = commandProcessor;
    private readonly ICommandParser _commandParser = commandParser;
    private readonly IUiRenderer _uiRenderer = uiRenderer;
    private readonly KeyboardInterceptor _keyboardInterceptor = keyboardInterceptor;
    private readonly GameStatsTracker _gameStatsTracker = gameStatsTracker;
    private readonly EventsFlowObserver _eventsFlowObserver = eventsFlowObserver;

    public void Execute()
    {
        Initialize();
        
        _uiRenderer.RenderWelcomeScreen();
        _uiRenderer.RenderInstructionsScreen();
        _keyboardInterceptor.WaitForKeyPress();
        _uiRenderer.ClearScreen();

        var executeNextRound = true;
        
        while (executeNextRound)
        {
            var command = GetCommand();
            
            if (!_commandProcessor.CanExecute(command))
            {
                _uiRenderer.RenderWarning("This command can not be executed now");
                continue;
            }

            var commandCompleted = _commandProcessor.Execute(command);
            if (!commandCompleted)
            {
                _uiRenderer.RenderError($"Error occured while executing '{command.Name}' command");
            }
            
            if (_gameEngine.PuzzleSolved)
            {
                _uiRenderer.ClearScreen();
                _uiRenderer.RenderVictoryScreen(_gameEngine.GetCurrentBoard(), _gameStatsTracker.GetStatistics());

                executeNextRound = false;
            }
        }
    }

    private void Initialize()
    {
        _gameEngine.AddObserver(_eventsFlowObserver);
        _gameEngine.AddObserver(_gameStatsTracker);
    }

    private IGameCommand GetCommand()
    {
        IGameCommand? command = null!;

        while (command is null)
        {
            _uiRenderer.RenderInputRequest("Enter your command:");
            
            var commandInput = _keyboardInterceptor.ReadFromConsole();
            while (string.IsNullOrEmpty(commandInput))
            {
                _uiRenderer.RenderError("Please enter a valid command");
                commandInput = _keyboardInterceptor.ReadFromConsole();
            }

            command = _commandParser.Parse(commandInput);
            if (command is null)
            {
                _uiRenderer.RenderError("Invalid command");
            }
        }

        return command;
    }
}