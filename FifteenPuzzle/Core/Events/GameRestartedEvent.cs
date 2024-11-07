using FifteenPuzzle.Core.Interfaces;

namespace FifteenPuzzle.Core.Events;

public record GameRestartedEvent(DateTime Timestamp) : IGameEvent;