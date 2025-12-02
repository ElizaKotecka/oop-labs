using Simulator;

namespace Runner;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");

        //Console.WriteLine("OUTPUT TEST\n");
        //Creature c = new Elf("Elandor", 5, 3);
        //Console.WriteLine(c);

        //Creature o = new Orc("Gorbag", 10, 8);
        //Console.WriteLine(o);

        //TestElfsAndOrcs();
        //TestValidators();
        //TestObjectsToString();

        //Console.WriteLine("\n");
        //Console.ReadKey();

        Point p = new(10, 25);
        Console.WriteLine(p.Next(Direction.Right));          // (11, 25)
        Console.WriteLine(p.NextDiagonal(Direction.Right));  // (11, 24)

    }
    static void TestElfsAndOrcs()
    {
        Console.WriteLine("\nHUNT TEST\n");
        var o = new Orc() { Name = "Gorbag", Rage = 7 };
        o.Greeting();
        for (int i = 0; i < 10; i++)
        {
            o.Hunt();
            o.Greeting();
        }

        Console.WriteLine("\nSING TEST\n");
        var e = new Elf("Legolas", agility: 2);
        e.Greeting();
        for (int i = 0; i < 10; i++)
        {
            e.Sing();
            e.Greeting();
        }

        Console.WriteLine("\nPOWER TEST\n");
        Creature[] creatures = {
        o,
        e,
        new Orc("Morgash", 3, 8),
        new Elf("Elandor", 5, 3)
        };
        foreach (Creature creature in creatures)
        {
            Console.WriteLine($"{creature.Name,-15}: {creature.Power}");
        }
    }
    static void TestValidators()
    {
        Console.WriteLine("\nVALIDATORS TEST\n");

        Creature c = new Orc() { Name = "    Shrek    ", Level = 20 };
        c.Greeting();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Elf("  ", -5);
        c.Greeting();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Orc("  donkey ") { Level = 7 };
        c.Greeting();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Elf("Puss in Boots – a clever and brave cat.");
        c.Greeting();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Orc("a                          troll name", 5);
        c.Greeting();
        c.Upgrade();
        Console.WriteLine(c.Info);

        var a = new Animals() { Description = "    Cats " };
        Console.WriteLine(a.Info);

        a = new Animals() { Description = "Mice           are great", Size = 40 };
        Console.WriteLine(a.Info);
    }

    static void TestObjectsToString()
    {
        object[] myObjects = {
        new Animals() { Description = "dogs"},
        new Birds { Description = "  eagles ", Size = 10 },
        new Elf("e", 15, -3),
        new Orc("morgash", 6, 4)
        };
        Console.WriteLine("\nMy objects:");
        foreach (var o in myObjects) Console.WriteLine(o);
        /*
            My objects:
            ANIMALS: Dogs <3>
            BIRDS: Eagles (fly+) <10>
            ELF: E## [10][0]
            ORC: Morgash [6][4]
        */
    }
}
