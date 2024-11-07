using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Cli.Console;

public class ConsoleRenderer : IUiRenderer
{
    private const string HorizontalLine = "───────";
    private const string VerticalLine = "│";
    private const string Corner = "┼";

    private const ConsoleColor ErrorColor = ConsoleColor.Red;
    private const ConsoleColor WarningColor = ConsoleColor.Yellow;
    private const ConsoleColor InfoColor = ConsoleColor.Cyan;

    private const ConsoleColor BorderColor = ConsoleColor.DarkBlue;
    private const ConsoleColor NumberColor = ConsoleColor.White;
    private const ConsoleColor EmptyTileColor = ConsoleColor.Magenta;

    public void RenderError(string error)
    {
        WriteColored($"❌ Error: {error}", ErrorColor);
        System.Console.WriteLine();
    }

    public void RenderWarning(string message)
    {
        WriteColored($"⚠️ Warning: {message}", WarningColor);
        System.Console.WriteLine();
    }

    public void RenderInfo(string message)
    {
        WriteColored(message, InfoColor);
        System.Console.WriteLine();
    }

    public void RenderInputRequest(string message)
    {
        WriteColored($"🔤 {message} ", InfoColor, false);
    }

    public void ClearScreen()
    {
        System.Console.Clear();
    }

    public void RenderWelcomeScreen()
    {
        System.Console.Clear();
        RenderInfo(Messages.WelcomeScreen);

        Thread.Sleep(1500);
        System.Console.Clear();
    }

    public void RenderVictoryScreen(IBoard board)
    {
        System.Console.Clear();
        RenderBoard(board);
        RenderInfo(Messages.VictoryScreen);

        RenderInputRequest("Press any key to exit...");
        System.Console.ReadKey(true);
    }

    public void RenderInstructionsScreen()
    {
        System.Console.Clear();
        RenderInfo(Messages.Instructions); 

        RenderInputRequest("Press any key to continue...");
    }
    
    public void RenderBoard(IBoard board)
    {
        System.Console.WriteLine();
        RenderHorizontalBorder();

        for (var row = 0; row < 4; row++)
        {
            RenderRow(row, board);
            RenderHorizontalBorder();
        }

        System.Console.WriteLine();
    }

    private void RenderCell(int number)
    {
        var cellContent = number == -1 
            ? "   *   "
            : number < 10 
                ? "   " + number + "   "
                : "  " + number + "   ";
            
        var color = number == -1 ? EmptyTileColor : NumberColor;
        WriteColored(cellContent, color, false);
    }

    private void RenderRow(int row, IBoard board)
    {
        WriteColored(VerticalLine, BorderColor, false);
        
        for (var col = 0; col < 4; col++)
        {
            var number = board.Grid[row * 4 + col];
            RenderCell(number);
            WriteColored(VerticalLine, BorderColor, false);
        }
        System.Console.WriteLine();
    }

    private void RenderHorizontalBorder()
    {
        WriteColored(Corner, BorderColor, false);
        for (var i = 0; i < 4; i++)
        {
            WriteColored(HorizontalLine, BorderColor, false);
            WriteColored(Corner, BorderColor, false);
        }
        System.Console.WriteLine();
    }

    private void WriteColored(string text, ConsoleColor color, bool writeLine = true)
    {
        System.Console.ForegroundColor = color;

        if (writeLine)
            System.Console.WriteLine(text);
        else
            System.Console.Write(text);

        System.Console.ResetColor();
    }
}