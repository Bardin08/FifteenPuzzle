namespace FifteenPuzzle.Core.Interfaces;

public interface IGameRenderer
{
    void RenderError(string error);
    void RenderBoard(IBoard board);
}