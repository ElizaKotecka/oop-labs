namespace Simulator.Maps;

public interface IMappable
{
    Map? Map { get; }
    Point Position { get; }
    char MapSymbol { get; }
    void Go(Direction direction);
    void InitMapAndPosition(Map map, Point startingPosition);
}
