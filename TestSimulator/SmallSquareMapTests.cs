using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallSquareMapTests
{
    [Fact]
    public void Constructor_ShouldCreateMap_IfSizeIsCorrect()
    {
        var map = new SmallSquareMap(10);
        Assert.Equal(10, map.Size);
    }

    [Fact]
    public void Constructor_ShouldThrowException_IfSizeIsTooSmall()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(4));
    }

    [Fact]
    public void Next_ShouldMove_IfSpaceAvailable()
    {
        var map = new SmallSquareMap(10);
        var start = new Point(5, 5);
        var next = map.Next(start, Direction.Up);

        Assert.Equal(new Point(5, 6), next);
    }

    [Fact]
    public void Next_ShouldStay_IfHitWall()
    {
        var map = new SmallSquareMap(10); // Mapa ma indeksy 0..9
        var start = new Point(9, 9);

        // Próba ruchu w górę powinna zostać zablokowana przez ścianę
        var next = map.Next(start, Direction.Up);

        // Punkt powinien pozostać ten sam
        Assert.Equal(start, next);
    }

    [Fact]
    public void NextDiagonal_ShouldStay_IfHitCorner()
    {
        var map = new SmallSquareMap(10);
        var start = new Point(9, 9);

        // Próba ruchu po skosie (Up -> Góra-Prawo) poza mapę
        var next = map.NextDiagonal(start, Direction.Up);

        Assert.Equal(start, next);
    }
}