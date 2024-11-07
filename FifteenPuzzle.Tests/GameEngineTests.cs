using FifteenPuzzle.Core.Services;

namespace FifteenPuzzle.Tests;

public class GameEngineTests
{
    private readonly GameEngine _sut = new();

    [Fact]
    public void Initialize_BoardShuffled_InitializesCorrectly()
    {
        _sut.Initialize();
        var isOrdered = _sut.PuzzleSolved;
        Assert.False(isOrdered);
    }
}