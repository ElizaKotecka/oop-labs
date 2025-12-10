using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<Creature> Creatures { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves.
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves,
    /// next move is again for first creature and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished { get; private set; } = false;

    /// <summary>
    /// Private list of VALID directions parsed from Moves string.
    /// </summary>
    private readonly List<Direction> _validMoves;

    /// <summary>
    /// Index of the current move in the _validMoves list.
    /// </summary>
    private int _currentTurnIndex = 0;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public Creature CurrentCreature => Creatures[_currentTurnIndex % Creatures.Count];

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName
    {
        get
        {
            if (Finished) return string.Empty;
            // Zwracamy nazwę kierunku (np. "left", "up") zamiast literki ze stringa,
            // ponieważ ignorujemy błędne znaki.
            return _validMoves[_currentTurnIndex].ToString().ToLower();
        }
    }

    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<Creature> creatures, List<Point> positions, string moves)
    {
        // 1. Walidacja danych wejściowych
        if (creatures == null || creatures.Count == 0)
            throw new ArgumentException("Lista stworów nie może być pusta.");

        if (positions == null || positions.Count != creatures.Count)
            throw new ArgumentException("Liczba pozycji startowych musi odpowiadać liczbie stworów.");

        Map = map ?? throw new ArgumentNullException(nameof(map));
        Creatures = creatures;
        Positions = positions;
        Moves = moves;

        // 2. Parsowanie ruchów RAZ na początku 
        _validMoves = DirectionParser.Parse(moves);

        // Jeśli nie ma żadnych poprawnych ruchów, symulacja jest od razu skończona
        if (_validMoves.Count == 0)
        {
            Finished = true;
        }

        // 3. Inicjalizacja stworów na mapie
        for (int i = 0; i < Creatures.Count; i++)
        {
            Creatures[i].InitMapAndPosition(Map, Positions[i]);
        }
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Symulacja jest zakończona.");

        // Pobieramy kierunek z listy wcześniej sparsowanych ruchów
        Direction direction = _validMoves[_currentTurnIndex];

        // Wykonujemy ruch stwora
        // CurrentCreature jest obliczany automatycznie przez getter: Creatures[index % Count]
        CurrentCreature.Go(direction);

        // Przesuwamy indeks tury
        _currentTurnIndex++;

        // Sprawdzamy, czy to był ostatni ruch
        if (_currentTurnIndex >= _validMoves.Count)
        {
            Finished = true;
        }
    }
}