using Simulator;

namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public int Size { get; }

    public SmallSquareMap(int size) : base(size, size)

    {
        if (size > 20)
            throw new ArgumentOutOfRangeException(nameof(size), "Rozmiar mapy musi byæ mniejszy ni¿ 20");

        Size = size;
    }

    public override Point Next(Point p, Direction d)
    {
            Point candidate = p.Next(d);

            return Exist(candidate) ? candidate : p;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point candidate = p.NextDiagonal(d);

        return Exist(candidate) ? candidate : p;
    }
}
