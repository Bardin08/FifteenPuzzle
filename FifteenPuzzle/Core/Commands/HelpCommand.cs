using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Commands;

public record HelpCommand : IGameCommand
{
    private readonly IUiRenderer _renderer;

    public HelpCommand(IUiRenderer renderer)
    {
        _renderer = renderer;
    }

    public string Name => "help";
    public string Description => "Display available commands and usage";

    public bool Execute()
    {
        _renderer.RenderInstructionsScreen();
        return true;
    }
}