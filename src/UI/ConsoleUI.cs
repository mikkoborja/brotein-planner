using System.Drawing;

public static class ConsoleUI {
    public static void PrintColoredText(string text, ConsoleColor color) {
        Console.ForegroundColor = color;
        Console.WriteLine();
        Console.WriteLine(text);
        Console.ResetColor();
    }
}