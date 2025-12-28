using SimConsole;
using Simulator.Maps;

namespace Simulator;

public class MapVisualizer
{
    private readonly Map _map;

    public MapVisualizer(Map map)
    {
        _map = map;
    }

    public void Draw()
    {
        int width = _map.SizeX;
        int height = _map.SizeY;

        // Rysujemy górę ramki
        DrawLine(Box.TopLeft, Box.Horizontal, Box.TopMid, Box.TopRight, width);

        // Rysujemy wiersze OD GÓRY (max Y) DO DOŁU (0), 
        // aby (0,0) wylądowało na dole ekranu.
        for (int y = height - 1; y >= 0; y--)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < width; x++)
            {
                var symbol = GetCellSymbol(x, y);
                Console.Write($" {symbol} {Box.Vertical}");
            }
            Console.WriteLine();

            // Rysujemy linię oddzielającą, chyba że to ostatni wiersz (podłoga)
            if (y > 0)
            {
                DrawLine(Box.MidLeft, Box.Horizontal, Box.Cross, Box.MidRight, width);
            }
        }

        // Rysujemy dół ramki
        DrawLine(Box.BottomLeft, Box.Horizontal, Box.BottomMid, Box.BottomRight, width);
    }

    /// <summary>
    /// Pobiera symbol do wyświetlenia w danej kratce.
    /// </summary>
    private char GetCellSymbol(int x, int y)
    {
        var creatures = _map.At(x, y).ToList(); // .ToList() wymaga 'using System.Linq;'

        if (creatures.Count == 0) return ' ';
        if (creatures.Count == 1) return creatures[0].MapSymbol;
        return 'X';
    }

    /// <summary>
    /// Metoda pomocnicza do rysowania linii poziomej z "łącznikami".
    /// </summary>
    private void DrawLine(char start, char mid, char cross, char end, int width)
    {
        Console.Write(start);
        for (int i = 0; i < width; i++)
        {
            // Rysujemy 3 poziome kreski na jedną kratkę (dla estetyki)
            Console.Write(mid);
            Console.Write(mid);
            Console.Write(mid);

            // Jeśli to nie ostatnia kolumna, stawiamy łącznik (krzyżyk lub trójnik)
            if (i < width - 1)
            {
                Console.Write(cross);
            }
        }
        Console.WriteLine(end);
    }
}