using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Events;

public record GameStartedEvent(IBoard InitBoard, DateTime Timestamp) : IGameEvent;