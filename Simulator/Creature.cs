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
    string Go(Direction direction) => $"{direction.ToString().ToLower()}";

    public string[] Go(Direction[] directions)
    {
        var results = new string[directions.Length];
        for (int i = 0; i < directions.Length; i++)
        {
            results[i] = Go(directions[i]);
        }
        return results;
    }

    public string[] Go(string directionsString)
    {
        var dirs = DirectionParser.Parse(directionsString);
        return Go(dirs);
    }

    //public virtual void SayHi() // virtual - dobiera metode na podstawie typu podstawionego pod zmienna (polimorfizm)
    //{
    //    Console.WriteLine($"Hi, I'm {Name}, my level is {Level}.");
    //}

    public abstract string Greeting();

    public abstract int Power { get; }

    public abstract string Info { get; }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}
