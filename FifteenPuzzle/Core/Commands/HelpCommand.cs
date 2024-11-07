using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Commands;

public record HelpCommand : IGameCommand
{
    // this can be achieved with reflection to avoid manual commands declaration
    private readonly Dictionary<string, string> _availableCommands = new()
    {
        { "help", "Display available commands and usage" }
    };

    private readonly IUiRenderer _renderer;

    public HelpCommand(IUiRenderer renderer)
    {
        _renderer = renderer;
    }

    public string Name => "help";
    public string Description => "Display available commands and usage";

    public bool Execute()
    {
        _renderer.RenderInfo("\nAvailable Commands:");
        foreach (var commandDescriptor in _availableCommands)
        {
            _renderer.RenderInfo($"  {commandDescriptor.Key,-15} - {commandDescriptor.Value}");
        }

        return true;
    }
}