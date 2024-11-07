using FifteenPuzzle.Cli.Commands;
using FifteenPuzzle.Core.Commands;
using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;
using FifteenPuzzle.Core.Services;
using Moq;

namespace FifteenPuzzle.Tests;

public class CommandProcessorTests
{
    private readonly Mock<IGameEngine> _gameEngineMock;
    private readonly CommandProcessor _sut;

    public CommandProcessorTests()
    {
        _gameEngineMock = new Mock<IGameEngine>();
        _sut = new CommandProcessor(_gameEngineMock.Object);
    }

    [Theory]
    [MemberData(nameof(GeneralCommands))]
    public void CanExecute_RoundIsNotStarted_CanExecuteGeneralCommands(IGameCommand command)
    {
        _gameEngineMock.Setup(x => x.IsRunning).Returns(false);

        var canExecute = _sut.CanExecute(command);
        Assert.True(canExecute);
    }

    [Fact]
    public void CanExecute_RoundIsNotStarted_CanExecuteStartCommand()
    {
        _gameEngineMock.Setup(x => x.IsRunning).Returns(false);

        var startCommand = new StartCommand(null!);
        var canExecute = _sut.CanExecute(startCommand);

        Assert.True(canExecute);
    }

    [Theory]
    [MemberData(nameof(GeneralCommands))]
    public void CanExecute_RoundIsStarted_CanExecuteGeneralCommands(IGameCommand command)
    {
        _gameEngineMock.Setup(x => x.IsRunning).Returns(true);

        var canExecute = _sut.CanExecute(command);
        Assert.True(canExecute);
    }

    [Theory]
    [MemberData(nameof(GameLoopCommands))]
    public void CanExecute_RoundIsStarted_CanExecuteGameLoopCommands(IGameCommand command)
    {
        _gameEngineMock.Setup(x => x.IsRunning).Returns(true);

        var canExecute = _sut.CanExecute(command);
        Assert.True(canExecute);
    }

    [Fact]
    public void CanExecute_RoundIsStarted_CanNotExecuteStartCommand()
    {
        _gameEngineMock.Setup(x => x.IsRunning).Returns(true);

        var startCommand = new StartCommand(null!);
        var canExecute = _sut.CanExecute(startCommand);

        Assert.False(canExecute);
    }

    public static IEnumerable<object[]> GeneralCommands() => new List<object[]>
    {
        new object[] { new HelpCommand(renderer: null!) },
        new object[] { new QuitCommand() }
    };

    public static IEnumerable<object[]> GameLoopCommands() => new List<object[]>
    {
        new object[] { new RestartCommand(null!) },
        new object[] { new ShowCommand(null!, null!) },
        new object[] { new MoveTileCommand(null!, Move.Direction.Up) },
    };
}