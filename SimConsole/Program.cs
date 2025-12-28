using Simulator;
using Simulator.Maps;
using System.Text;

namespace SimConsole;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Wybierz symulację:");
        Console.WriteLine("1. Sim1 (Stwory)");
        Console.WriteLine("2. Sim2 (Zwierzęta)");
        Console.WriteLine("3. Sim3 (Historia - tury 5, 10, 15, 20)");

        char choice = Console.ReadKey(true).KeyChar;

        if (choice == '1') Sim1();
        else if (choice == '2') Sim2();
        else if (choice == '3') Sim3();
        else Console.WriteLine("Nieprawidłowy wybór.");
    }

    static void Sim1()
    {
        SmallSquareMap map = new(5);
        List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor")];
        List<Point> points = [new(2, 2), new(3, 1)];
        string moves = "dlrludl";

        RunSimulation(map, creatures, points, moves);
    }

    static void Sim2()
    {
        SmallTorusMap map = new(8, 6);
        List<IMappable> creatures = [
            new Elf("Elandor"),
            new Orc("Gorbag"),
            new Animals { Description = "Rabbits", Size = 2 },
            new Birds { Description = "Eagles", Size = 3, CanFly = true },
            new Birds { Description = "Ostriches", Size = 5, CanFly = false }
        ];

        List<Point> points = [new(0, 0), new(7, 5), new(2, 2), new(4, 3), new(1, 1)];
        // 20 ruchów
        string moves = "ulrdulrdulrdulrdulrd";

        RunSimulation(map, creatures, points, moves);
    }
    static void Sim3()
    {
        // Konfiguracja taka sama jak w Sim2
        SmallTorusMap map = new(8, 6);
        List<IMappable> creatures = [
            new Elf("Elandor"),
        new Orc("Gorbag"),
        new Animals { Description = "Rabbits", Size = 2 },
        new Birds { Description = "Eagles", Size = 3, CanFly = true },
        new Birds { Description = "Ostriches", Size = 5, CanFly = false }
        ];
        List<Point> points = [new(0, 0), new(7, 5), new(2, 2), new(4, 3), new(1, 1)];
        string moves = "ulrdulrdulrdulrdulrd"; // 20 ruchów

        // 1. Tworzymy symulację
        Simulation simulation = new(map, creatures, points, moves);

        // 2. Tworzymy historię (to wykona całą symulację od razu)
        SimulationLog history = new(simulation);

        // 3. Wizualizator historii
        LogVisualizer visualizer = new(history);

        // 4. Wyświetlamy tylko konkretne tury
        int[] turnsToShow = { 5, 10, 15, 20 };

        Console.Clear();
        Console.WriteLine("=== REPLAY WYBRANYCH TUR ===\n");

        foreach (int turnIndex in turnsToShow)
        {
            visualizer.Draw(turnIndex);
            Console.WriteLine("\nNaciśnij Enter, aby zobaczyć kolejny wybrany krok...");
            Console.ReadLine();
        }
        Console.WriteLine("Koniec prezentacji historii.");
    }





    static void RunSimulation(Map map, List<IMappable> creatures, List<Point> points, string moves)
    {
        Simulation simulation = new(map, creatures, points, moves);
        MapVisualizer visualizer = new(simulation.Map);

        while (!simulation.Finished)
        {
            visualizer.Draw();

            var m = simulation.CurrentMappable;
            var move = simulation.CurrentMoveName;

            Console.WriteLine($"\nRUCH {simulation.CurrentMoveNumber}: {simulation.CurrentMappable.MapSymbol} {simulation.CurrentMappable} idzie w kierunku {simulation.CurrentMoveName}");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey(true); // 'true' sprawia, że kliknięty klawisz nie wyświetla się w konsoli
            simulation.Turn();
        }

        Console.Clear();
        Console.WriteLine("Symulacja zakończona");
        visualizer.Draw();
    }
}