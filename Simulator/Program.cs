using System.Timers;

namespace Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Simulator!\n");

            TestElfsAndOrcs();
            TestValidators();

            Console.WriteLine("\n");
            Console.ReadKey();

        }
        static void TestElfsAndOrcs()
        {
            Console.WriteLine("HUNT TEST\n");
            var o = new Orc() { Name = "Gorbag", Rage = 7 };
            o.SayHi();
            for (int i = 0; i < 10; i++)
            {
                o.Hunt();
                o.SayHi();
            }

            Console.WriteLine("\nSING TEST\n");
            var e = new Elf("Legolas", agility: 2);
            e.SayHi();
            for (int i = 0; i < 10; i++)
            {
                e.Sing();
                e.SayHi();
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
            Console.WriteLine("\n--- TEST VALIDATORS ---\n");

            Creature c = new Orc() { Name = "    Shrek    ", Level = 20 };
            c.SayHi();
            c.Upgrade();
            Console.WriteLine(c.Info);

            c = new Elf("  ", -5);
            c.SayHi();
            c.Upgrade();
            Console.WriteLine(c.Info);

            c = new Orc("  donkey ") { Level = 7 };
            c.SayHi();
            c.Upgrade();
            Console.WriteLine(c.Info);

            c = new Elf("Puss in Boots – a clever and brave cat.");
            c.SayHi();
            c.Upgrade();
            Console.WriteLine(c.Info);

            c = new Orc("a                          troll name", 5);
            c.SayHi();
            c.Upgrade();
            Console.WriteLine(c.Info);

            var a = new Animals() { Description = "    Cats " };
            Console.WriteLine(a.Info);

            a = new Animals() { Description = "Mice           are great", Size = 40 };
            Console.WriteLine(a.Info);
        }
    }
}
