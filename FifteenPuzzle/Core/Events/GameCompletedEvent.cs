using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Events;

public record GameCompletedEvent(DateTime Timestamp) : IGameEvent;