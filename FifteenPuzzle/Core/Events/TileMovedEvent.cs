using FifteenPuzzle.Core.Interfaces;
using FifteenPuzzle.Core.Models;

namespace FifteenPuzzle.Core.Events;

public record TileMovedEvent(Move Move, DateTime Timestamp) : IGameEvent;