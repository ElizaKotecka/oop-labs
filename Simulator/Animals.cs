using Simulator.Maps;

namespace Simulator;
public class Animals: IMappable
{
    private string _description = "Unknown";
    protected Map? map;
    protected Point point;

    public uint Size { get; set; } = 3;
    public string Description
    {
        get => _description;
        init => _description = Validator.ValidateDesc(value);
    }

    public Animals()
    {
    }

    public Animals(string description)
    {
        Description = description;
    }

    public virtual string Info => $"{Description} <{Size}>";

    public Map? Map => map;

    public Point Position => point;

    public virtual char MapSymbol => 'A';

    public override string ToString()
    {
        return Info;
    }

    public virtual void Go(Direction direction)
    {
        if (map is null)
            return;

        Point nextPoint = map.Next(point, direction);

        map.Move(this, point, nextPoint);

        point = nextPoint;
    }

    public void InitMapAndPosition(Map map, Point startingPosition)
    {
        if (map == null)
            throw new ArgumentNullException(nameof(map));
        if (!map.Exist(startingPosition))
            throw new ArgumentOutOfRangeException(nameof(startingPosition));
        if (this.map != null)
            throw new InvalidOperationException("Zwierzę już znajduje się na mapie");

        this.map = map;
        point = startingPosition;
        map.Add(this, startingPosition);
    }
}
