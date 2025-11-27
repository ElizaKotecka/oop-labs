namespace Simulator;

public class Orc : Creature
{
    private int _rage = 1;
    private int _huntCount = 0;

    public int Rage
    {
        get => _rage;
        init => _rage = Validator.Limiter(value, 0, 10);
    }

    public override int Power => 7 * Level + 3 * Rage;

    public void Hunt()
    {
        _huntCount++;
        if (_huntCount % 2 == 0 && _rage < 10)
        {
            _rage++;
        }
        Console.WriteLine($"{Name} is hunting.");
    }

    public Orc() : base()
    {
    }

    public Orc(string name, int level = 1, int rage = 1) : base(name, level)
    {
        Rage = rage; 
    }

    public override void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}, rage is {Rage}.");
    }
}
