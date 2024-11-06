using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Cli.Display;

public class ConsoleRenderer : IUiRenderer
{
    public void RenderError(string error)
    {
        throw new NotImplementedException();
    }

    public void RenderWarning(string message)
    {
        throw new NotImplementedException();
    }

    public void RenderInfo(string message)
    {
        throw new NotImplementedException();
    }

    public void RenderBoard(IBoard board)
    {
        throw new NotImplementedException();
    }
}