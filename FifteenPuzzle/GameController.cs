using FifteenPuzzle.Cli.Display;
using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle;

public class GameController(
    ICommandParser commandParser,
    IUiRenderer uiRenderer,
    ICommandProcessor commandProcessor)
{
    private readonly ICommandProcessor _commandProcessor = commandProcessor;
    private readonly ICommandParser _commandParser = commandParser;
    private readonly IUiRenderer _uiRenderer = uiRenderer;

    private readonly KeyboardInterceptor _keyboardInterceptor = new(uiRenderer);

    public void Execute()
    {
        _uiRenderer.RenderWelcomeScreen();
        _uiRenderer.RenderInstructionsScreen();
        _keyboardInterceptor.WaitForKeyPress();
        _uiRenderer.ClearScreen();

        while (true)
        {
            var command = GetCommand();
            
            if (!_commandProcessor.CanExecute(command))
            {
                _uiRenderer.RenderWarning("This command can not be executed now");
                continue;
            }

            command.Execute();

            // while (!_gameEngine.IsSolved())
            // {
            //     ShowPrompt();
            //     var input = Console.ReadLine();
            //     if (string.IsNullOrEmpty(input))
            //     {
            //         _uiRenderer.RenderWarning("Invalid input.");
            //         continue;
            //     }
            //
            //     HandleInput(input);
            // }
            //
            // _uiRenderer.RenderVictoryScreen(_gameEngine.GetCurrentBoard());
        }
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