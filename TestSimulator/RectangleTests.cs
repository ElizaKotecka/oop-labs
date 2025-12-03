using Simulator;

namespace TestSimulator;

public class RectangleTests
{
    [Fact]
    public void Constructor_ValidCoords_ShouldCreateRectangle()
    {
        var r = new Rectangle(0, 0, 10, 10);
        Assert.Equal(0, r.X1);
        Assert.Equal(10, r.X2);
    }

    [Fact]
    public void Constructor_ShouldSwapAutomatically()
    {
        // P1 to (10, 10), P2 to (0, 0)
        var r = new Rectangle(10, 10, 0, 0);
        Assert.Equal(0, r.X1);
        Assert.Equal(0, r.Y1);
        Assert.Equal(10, r.X2);
        Assert.Equal(10, r.Y2);
    }

    [Fact]
    public void Constructor_ThinRectangle_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(1, 2, 1, 5));
        Assert.Throws<ArgumentException>(() => new Rectangle(1, 2, 4, 2));
    }

    [Fact]
    public void Contains_ShouldReturnPointsCorrectly()
    {
        var rect = new Rectangle(0, 0, 5, 5);
        Assert.True(rect.Contains(new Point(0, 0)));
        Assert.True(rect.Contains(new Point(3, 4)));
        Assert.True(rect.Contains(new Point(5, 5)));
        Assert.False(rect.Contains(new Point(6, 0)));
        Assert.False(rect.Contains(new Point(0, 6)));
    }
}

