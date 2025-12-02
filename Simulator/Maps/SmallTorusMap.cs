namespace Simulator.Maps;

public class SmallTorusMap : Map
{
    public int Size { get; }

    public SmallTorusMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Size must be between 5 and 20.");
        }
        Size = size;
    }

    public override bool Exist(Point p)
    {
        // Punkt istnieje tylko jeśli mieści się w prostokącie (0,0) do (Size-1, Size-1)
        return p.X >= 0 && p.X < Size && p.Y >= 0 && p.Y < Size;
    }

    public override Point Next(Point p, Direction d)
    {
        var moved = p.Next(d);
        return NormalizePoint(moved);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        var moved = p.NextDiagonal(d);
        return NormalizePoint(moved);
    }

    // Metoda pomocnicza do "zawijania" punktów (logika torusa)
    private Point NormalizePoint(Point p)
    {
        // Wzór (x % n + n) % n obsługuje poprawnie liczby ujemne
        // Np. dla Size=20: (-1 + 20) % 20 = 19
        int newX = (p.X + Size) % Size;
        int newY = (p.Y + Size) % Size;

        return new Point(newX, newY);
    }
}