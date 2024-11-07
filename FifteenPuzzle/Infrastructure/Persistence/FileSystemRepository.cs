using System.Text;
using System.Text.Json;
using FifteenPuzzle.Infrastructure.Configuration;
using FifteenPuzzle.Infrastructure.Persistence.Models;

namespace FifteenPuzzle.Infrastructure.Persistence;

public interface IFileSystemRepository
{
    void Initialize(List<int> initialBoard);
    void Reset();
    void AddMove(MoveDescriptor move);
    void GameComplete(DateTime completedAt);
    void Save();
}

public class FileSystemRepository : IFileSystemRepository, IDisposable
{
    private GameDescriptor? _gameDescriptor;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(15));
    private readonly CancellationTokenSource _cts = new();

    public FileSystemRepository()
    {
        Task.Run(() => SaveBackgroundTask(_cts.Token), _cts.Token);
    }

    public void Initialize(List<int> initialBoard)
        => _gameDescriptor = new GameDescriptor
        {
            GameId = DateTimeOffset.Now.ToUnixTimeMilliseconds() + "_" + Guid.NewGuid().ToString("N")[..7],
            Result = GameResult.Unsolved,
            Moves = [],
            InitialBoard = initialBoard,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now
        };

    public void Reset()
    {
        if (_gameDescriptor is null) return;

        _gameDescriptor.Result = GameResult.Unsolved;
        _gameDescriptor.Moves = [];
        _gameDescriptor.CreatedAt = _gameDescriptor.UpdatedAt = DateTimeOffset.Now;
    }

    public void AddMove(MoveDescriptor move)
    {
        if (_gameDescriptor is null) return;

        _gameDescriptor.Moves.Push(move);
        _gameDescriptor.UpdatedAt = DateTimeOffset.Now;
    }

    public void GameComplete(DateTime completedAt)
    {
        if (_gameDescriptor is null) return;

        _gameDescriptor.Result = GameResult.Solved;
        _gameDescriptor.UpdatedAt = new DateTimeOffset(completedAt);
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(_gameDescriptor);
        File.WriteAllText(GameSettings.GameFolder, json, Encoding.UTF8);
    }

    private async Task SaveBackgroundTask(CancellationToken ct)
    {
        Directory.CreateDirectory(GameSettings.GameFolder);
        
        while (await _timer.WaitForNextTickAsync(ct))
        {
            if (_gameDescriptor is null)
                continue;

            var path = Path.Combine(GameSettings.GameFolder, $"{_gameDescriptor.GameId}.json");
            var json = JsonSerializer.Serialize(_gameDescriptor);
            await File.WriteAllTextAsync(path, json, Encoding.UTF8, ct);
        }
    }

    public void Dispose()
    {
        _timer.Dispose();
        _cts.Dispose();
    }
}