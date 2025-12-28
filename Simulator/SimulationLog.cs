using Simulator.Maps;
using System.Linq;

namespace Simulator;

public class SimulationLog
{
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<TurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0

    public SimulationLog(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        // KROK 0: Stan początkowy przed jakimkolwiek ruchem
        TurnLogs.Add(new TurnLog
        {
            Mappable = "None",
            Move = "Initial State",
            Symbols = GetCurrentMapSymbols()
        });

        // Wykonujemy symulację do końca
        while (!_simulation.Finished)
        {
            // Zapamiętujemy dane obiektu, który ZA CHWILĘ wykona ruch
            string mover = _simulation.CurrentMappable.ToString();
            string moveName = _simulation.CurrentMoveName;

            // Wykonujemy ruch
            _simulation.Turn();

            // Zapisujemy stan mapy PO wykonaniu ruchu
            TurnLogs.Add(new TurnLog
            {
                Mappable = mover,
                Move = moveName,
                Symbols = GetCurrentMapSymbols()
            });
        }
    }

    /// <summary>
    /// Pobiera aktualne symbole wszystkich obiektów na mapie.
    /// </summary>
    private Dictionary<Point, char> GetCurrentMapSymbols()
    {
        var symbols = new Dictionary<Point, char>();

        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                var mappables = _simulation.Map.At(x, y);
                if (mappables != null && mappables.Any())
                {
                    // Logika identyczna jak w MapVisualizer:
                    // Jeśli więcej niż jeden obiekt na polu -> 'X', w przeciwnym razie symbol obiektu
                    char symbol = mappables.Count() > 1 
                        ? 'X' 
                        : mappables.First().MapSymbol;
                    
                    symbols[new Point(x, y)] = symbol;
                }
            }
        }
        return symbols;
    }
}