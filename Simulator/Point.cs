namespace Simulator;

public readonly struct Point
{
    public readonly int X, Y;
    public Point(int x, int y) => (X, Y) = (x, y);
    public override string ToString() => $"({X}, {Y})";

    // Zwraca nowy punkt przesuniêty o jedno pole w podanym kierunku
    public Point Next(Direction direction) => direction switch
    {
        Direction.Up => new Point(X, Y + 1),
        Direction.Right => new Point(X + 1, Y),
        Direction.Down => new Point(X, Y - 1),
        Direction.Left => new Point(X - 1, Y),
        _ => this
    };

    // Zwraca nowy punkt przesuniêty po skosie (45 stopni zgodnie z ruchem wskazówek zegara od kierunku)
    public Point NextDiagonal(Direction direction) => direction switch
    {
        // 45 stopni od Góry (zgodnie z zegarem) to Góra-Prawo
        Direction.Up => new Point(X + 1, Y + 1),

        // 45 stopni od Prawej (zgodnie z zegarem) to Dó³-Prawo 
        // (To pasuje do przyk³adu: X roœnie, Y maleje)
        Direction.Right => new Point(X + 1, Y - 1),

        // 45 stopni od Do³u (zgodnie z zegarem) to Dó³-Lewo
        Direction.Down => new Point(X - 1, Y - 1),

        // 45 stopni od Lewej (zgodnie z zegarem) to Góra-Lewo
        Direction.Left => new Point(X - 1, Y + 1),

        _ => this
    };
}