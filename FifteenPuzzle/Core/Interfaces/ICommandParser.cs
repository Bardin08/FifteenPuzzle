using FifteenPuzzle.Core.Commands;

namespace FifteenPuzzle.Core.Interfaces;

public interface ICommandParser
{
    IGameCommand? Parse(string input);
}


// user input
//      -> parse (validate, convert to command)
//      -> run command
//      -> dispay board state
// 
