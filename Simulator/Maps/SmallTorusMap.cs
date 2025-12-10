namespace Simulator.Maps;

public class SmallTorusMap : Map
{

    public SmallTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20)
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Rozmiar mapy musi być mniejszy niż 20");

        if (sizeY > 20)
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Rozmiar mapy musi być mniejszy niż 20.");
    
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
        int newX = (p.X + SizeX) % SizeX;
        int newY = (p.Y + SizeY) % SizeY;

        return new Point(newX, newY);
    }
}