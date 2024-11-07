namespace FifteenPuzzle.Infrastructure.Persistence.Models;

public class GameDescriptor
{
    public required string GameId { get; set; }
    public GameResult Result { get; set; }
    public required Stack<MoveDescriptor> Moves { get; set; }
    public required List<int> InitialBoard { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public enum GameResult
{
    Unsolved = 0,
    Solved = 1,
}

public record MoveDescriptor
{
    public int TileNumber { get; set; }
    public int Direction { get; set; }
    public long Timestamp { get; set; }
}