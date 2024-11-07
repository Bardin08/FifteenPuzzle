using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Cli.Display;

public class KeyboardInterceptor(IUiRenderer uiRenderer)
{
    private readonly IUiRenderer _uiRenderer = uiRenderer;

    public void WaitForKeyPress(ConsoleKeyInfo? keyInfo = null, bool notifyOnWrongKey = false)
    {
        var pressedKey = Console.ReadKey(true);

        if (keyInfo is null)
            return;

        while (pressedKey != keyInfo)
        {
            if (notifyOnWrongKey)
                _uiRenderer.RenderError("Unexpected key pressed. Try again");

            pressedKey = Console.ReadKey(true);
        }
    }

    public string? ReadFromConsole() => Console.ReadLine();
}