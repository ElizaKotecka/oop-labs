using Simulator;
using Simulator.Maps;
using System.Text;

namespace SimConsole;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        SmallSquareMap map = new(5);
        //zmieniamy Creatures na IMappable
        List<Creature> creatures = [   
            new Orc("Gorbag"),
            new Elf("Elandor")
        ];
        List<Point> points = [new(2, 2), new(3, 1)];
        string moves = "dlrludl";

        Simulation simulation = new(map, creatures, points, moves);
        MapVisualizer visualizer = new(simulation.Map);

        // Pętla symulacji
        while (!simulation.Finished)
        {
            // Rysowanie mapy
            visualizer.Draw();

            Console.WriteLine("\nNaciśnij Enter, aby wykonać ruch...");
            Console.ReadKey();

            simulation.Turn();
        }

        // Stan końcowy po wyjściu z pętli
        Console.Clear();
        Console.WriteLine("Symualcja zakończona");

        visualizer.Draw();
    }
}