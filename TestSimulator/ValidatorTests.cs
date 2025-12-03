using Simulator;

namespace TestSimulator;

public class ValidatorTests
{
    [Theory]
    [InlineData(5, 0, 10, 5)]   // W zakresie
    [InlineData(-5, 0, 10, 0)]  // Poniżej min -> min
    [InlineData(15, 0, 10, 10)] // Powyżej max -> max
    public void Limiter_ShouldClampValues(int value, int min, int max, int expected)
    {
        var result = Validator.Limiter(value, min, max);
        Assert.Equal(expected, result);
    }

    [Theory]
    // 1. Zwykły przypadek (trim + wielka litera)
    [InlineData("  shrek  ", 3, 10, '#', "Shrek")]

    // 2. Zbyt krótkie (padding)
    [InlineData("ab", 5, 10, '#', "Ab###")]

    // 3. Zbyt długie (ucięcie)
    [InlineData("BardzoDlugaNazwa", 3, 6, '#', "Bardzo")]

    // 4. Puste/Null -> padding placeholderem
    [InlineData("", 3, 5, '*', "***")]
    [InlineData(null, 3, 5, '*', "***")]

    // 5. Znak na początku nie będący literą
    [InlineData("123", 5, 10, 'x', "123xx")]

    // 6. Same spacje
    [InlineData("   ", 3, 5, '?', "???")]
    public void Shortener_ShouldProcessStringCorrectly(string value, int min, int max, char placeholder, string expected)
    {
        var result = Validator.Shortener(value, min, max, placeholder);
        Assert.Equal(expected, result);
    }
}