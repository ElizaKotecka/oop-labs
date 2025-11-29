namespace Simulator;

public abstract class Creature // dzieki abstract klasa sluzy tylko do tworzenia obiektow potomnych: nie zadziała Creature.c = new Creature()
{
    private string _name;
    private int _level;

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
        string lowerDirection = direction.ToString().ToLower();

        Console.WriteLine($"{Name} goes {lowerDirection}.");
    }

    public void Go(Direction[] directions)
    {
        foreach (Direction direction in directions)
        {
            Go(direction);
        }
    }

    public void Go(string directionsString)
    {
        Direction[] directions = DirectionParser.Parse(directionsString);

        // druga metoda Go do ruchów
        Go(directions);
    }

    //public virtual void SayHi() // virtual - dobiera metode na podstawie typu podstawionego pod zmienna (polimorfizm)
    //{
    //    Console.WriteLine($"Hi, I'm {Name}, my level is {Level}.");
    //}

    public abstract void SayHi();

    public abstract int Power { get; }

    public abstract string Info { get; }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}
