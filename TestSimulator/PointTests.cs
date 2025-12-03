using Simulator;

namespace TestSimulator;

public class PointTests
{
    [Fact]
    public void Next_ShouldMoveUpCorrectly()
    {
        var p = new Point(10, 10);
        var next = p.Next(Direction.Up);
        // Up to Y + 1
        Assert.Equal(new Point(10, 11), next);
    }

    [Fact]
    public void Next_ShouldMoveRightCorrectly()
    {
        var p = new Point(10, 10);
        var next = p.Next(Direction.Right);
        Assert.Equal(new Point(11, 10), next);
    }

    [Fact]
    public void NextDiagonal_ShouldMoveUpRight()
    {
        var p = new Point(10, 10);
        // NextDiagonal(Up) to skos w prawo-górę
        var next = p.NextDiagonal(Direction.Up);
        Assert.Equal(new Point(11, 11), next);
    }

    [Fact]
    public void NextDiagonal_ShouldMoveDownRight()
    {
        var p = new Point(10, 10);
        // NextDiagonal(Right) to skos w prawo-dół
        var next = p.NextDiagonal(Direction.Right);
        Assert.Equal(new Point(11, 9), next);
    }
}