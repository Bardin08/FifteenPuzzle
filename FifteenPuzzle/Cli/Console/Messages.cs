namespace FifteenPuzzle.Cli.Console;

public static class Messages
{
    public const string WelcomeScreen =
        """
        ╔═══════════════════════════════════════╗
        ║        Welcome to 15 Puzzle! 🎮       ║
        ║    Can you solve this classic game?   ║
        ╚═══════════════════════════════════════╝
        """;

    public const string VictoryScreen =
        """
        🎉 Congratulations! You've solved the puzzle! 🎉
        ╔═══════════════════════════════════════╗
        ║           Puzzle Completed! 🏆        ║
        ║         You're a puzzle master!       ║
        ╚═══════════════════════════════════════╝
        ║           Total Moves: {0}       ║
        ║         Game Duration: {1}       ║
        ╚═══════════════════════════════════════╝
        """;

    public const string Instructions =
        """
        How to play:
        - Move tiles using these commands:
          - 'move up', 'move down', 'move left', 'move right' to move empty cell
        - Game control:
          - 'start' to start new game
          - 'restart' to reset current puzzle
          - 'quit' to exit game
        - Display commands:
          - 'show' to display current board
          - 'stats' to show your progress
          - 'help' to see these instructions
        - Goal: Arrange numbers from 1 to 15 in order! 🎯
        
        Good luck! 🍀
        """;
}