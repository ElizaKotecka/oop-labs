using Simulator;

namespace Simulator.Maps;

/// <summary>
/// Map of points.
/// </summary>
public abstract class Map
{

    public readonly int SizeX;
    public readonly int SizeY;
    private readonly Rectangle area;
    private readonly Dictionary<Point, List<IMappable>> creatures = new();

    protected Map(int sizeX, int sizeY)
    {
        if (sizeX < 5)
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Rozmiar mapy musi byæ wiêkszy ni¿ 5.");
        if (sizeY < 5)
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Rozmiar mapy musi byæ wiêkszy ni¿ 5.");

        SizeX= sizeX;
        SizeY = sizeY;
        area = new Rectangle(0, 0, SizeX - 1, SizeY - 1);
    }

    /// <summary>
    /// Check if give point belongs to the map.
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns></returns>
    public virtual bool Exist(Point p)
    {
        return area.Contains(p);
    }


    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point NextDiagonal(Point p, Direction d);

    /// <summary>
    /// Add creature to the map at point p.
    /// </summary>
    /// <param name="creature"> Creature to place on the map </param>
    /// <param name="p">Point where creature apeares</param>
    public void Add(IMappable m, Point p)
    {
        if (!creatures.TryGetValue(p, out var list))
        {
            list = new List<IMappable>();
            creatures[p] = list;
        }
        list.Add(m);
    }


    /// <summary>
    /// Removing creature from the map.
    /// </summary>
    /// <param name="creature">Vreature we are removing from the map (maybe it died :(((( )</param>
    public void Remove(IMappable m, Point p)
    {
        if (creatures.TryGetValue(p, out var list))
        {
            list.Remove(m);
            if (list.Count == 0)
                creatures.Remove(p);
        }
    }


    public void Move(IMappable m, Point from, Point to)
    {
        Remove(m, from);
        Add(m, to);
    }


    /// <summary>
    /// Get list of creatures
    /// </summary>
    /// <param name="p"> point to check</param>
    /// <returns></returns>
    public IEnumerable<IMappable> At(Point p)
    {
        if (creatures.TryGetValue(p, out var list))
            return list;
        return Array.Empty<IMappable>();
    }

    public IEnumerable<IMappable> At(int x, int y)
    => At(new Point(x, y));
}