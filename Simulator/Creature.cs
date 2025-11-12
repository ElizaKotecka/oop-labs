namespace Simulator;

public class Creature
{
    private string _name;
    private int _level;
    public string Name
    {
        get => _name;
        init => _name = ValidateAndFormatName(value);
    }

    public int Level
    {
        get => _level;
        init => _level = ValidateLevel(value);
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

    private string ValidateAndFormatName(string inputName)
    {
        string processedName = (inputName ?? "").Trim(); // (inputName ?? "") obsługuje null

        if (processedName.Length > 25)
        {
            processedName = processedName.Substring(0, 25).TrimEnd();
        }

        if (processedName.Length < 3)
        {
            processedName = processedName.PadRight(3, '#');
        }

        if (processedName.Length > 0 && char.IsLower(processedName[0]))
        {
            processedName = char.ToUpper(processedName[0]) + processedName.Substring(1);
        }

        return processedName;

    }

    private int ValidateLevel(int inputLevel)
    {
        if (inputLevel < 1) return 1;
        if (inputLevel > 10) return 10;
        return inputLevel;
    }

    public void Upgrade()
    {
        if (Level < 10)
        {
            _level += 1;
        }
    }

    public void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}.");
    }
    public string Info => $"{Name}, [{Level}]";
}
