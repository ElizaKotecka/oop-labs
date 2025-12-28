namespace Simulator;

public class Elf : Creature
{
    private int _agility = 1;
    private int _singCount = 0;

    public int Agility
    {
        get => _agility;
        init => _agility = Validator.Limiter(value, 0, 10);
    }

    public override int Power => 8 * Level + 2 * Agility;

    public void Sing()
    {
        _singCount++;
        if (_singCount % 3 == 0 && _agility < 10)
            _agility = Validator.Limiter(Agility + 1, 0, 10);
    }

    public Elf() : base()
    {
    }

    public Elf(string name, int level = 1, int agility = 1) : base(name, level) // najpierw probuje wywolac konstruktor Creature (bazowy) zawsze najpierw bezparametrowy
    {
        Agility = agility;
    }

    public override string Greeting() // override - przyslon metode klasy bazowej (zeby to virtual w Creature dzialalo)
    {
        return $"Hi, I'm {Name}, my level is {Level}, and my agility is {Agility}.";
    }

    public override string Info => $"{Name} [{Level}][{Agility}]";

    public override char MapSymbol => 'E';
}
