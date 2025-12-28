using Simulator;
using Simulator.Maps;

namespace SimConsole;

internal class LogVisualizer
{
    SimulationLog Log { get; }

    public LogVisualizer(SimulationLog log)
    {
        Log = log;
    }

    public void Draw(int turnIndex)
    {
        if (turnIndex < 0 || turnIndex >= Log.TurnLogs.Count)
        {
            Console.WriteLine("Błędny numer tury!");
            return;
        }

        var turn = Log.TurnLogs[turnIndex];
        int width = Log.SizeX;
        int height = Log.SizeY;

        // Rysujemy nagłówek tury
        Console.WriteLine($"TURA {turnIndex}: {turn.Mappable} -> {turn.Move}");

        DrawLine(Box.TopLeft, Box.Horizontal, Box.TopMid, Box.TopRight, width);

        for (int y = height - 1; y >= 0; y--)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < width; x++)
            {
                // Pobieramy symbol ze słownika zapisanego w Logu
                char symbol = turn.Symbols.GetValueOrDefault(new Point(x, y), ' ');
                Console.Write($" {symbol} {Box.Vertical}");
            }
            Console.WriteLine();
            if (y > 0) DrawLine(Box.MidLeft, Box.Horizontal, Box.Cross, Box.MidRight, width);
        }

        DrawLine(Box.BottomLeft, Box.Horizontal, Box.BottomMid, Box.BottomRight, width);
    }

    private void DrawLine(char start, char mid, char cross, char end, int width)
    {
        Console.Write(start);
        for (int i = 0; i < width; i++)
        {
            Console.Write($"{mid}{mid}{mid}");
            if (i < width - 1) Console.Write(cross);
        }
        Console.WriteLine(end);
    }
}
