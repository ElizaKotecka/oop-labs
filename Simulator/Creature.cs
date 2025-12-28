using Simulator.Maps;

namespace Simulator;

public abstract class Creature : IMappable
{
    private string _name;
    private int _level;

    private Map? map;
    private Point point;

    public string Name
    {
        get => _name;
        init => _name = Validator.Shortener(value, 3, 25, '#');
    }

    public int Level
    {
        get => _level;
        init => _level = Validator.Limiter(value, 1, 10);
    }

    public Point Position => point;
    public Map? Map => Map;
    public virtual char MapSymbol => '?';

    public void InitMapAndPosition(Map map, Point startingPosition)
    {
        if (map == null)
            throw new ArgumentNullException(nameof(map));
        if (!map.Exist(startingPosition))
            throw new ArgumentOutOfRangeException(nameof(startingPosition));
        if (this.map != null)
            throw new InvalidOperationException("Stwór już znajduję się na mapie");

        this.map = map;
        point = startingPosition;
        map.Add(this, startingPosition);
    }

    public Creature()
    {
        Name = "Unknown";
        Level = 1;
    }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public void Upgrade()
    {
        if (Level < 10)
        {
            _level += 1;
        }
    }

    public void Go(Direction direction)
    {
        if (map is null)
            return;

        Point nextPoint = map.Next(point, direction);

        map.Move(this, point, nextPoint);

        point = nextPoint;
    }

    public abstract string Greeting();

    public abstract int Power { get; }

    public abstract string Info { get; }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}
