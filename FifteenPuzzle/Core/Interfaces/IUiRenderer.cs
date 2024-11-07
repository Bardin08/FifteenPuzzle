namespace FifteenPuzzle.Core.Interfaces;

public interface IUiRenderer
{
    void RenderBoard(IBoard board);
    void RenderError(string message);
    void RenderWarning(string message);
    void RenderInfo(string message);
    void RenderInputRequest(string message);
    void ClearScreen();

    void RenderWelcomeScreen();
    void RenderVictoryScreen(IBoard board);
    void RenderInstructionsScreen();
}