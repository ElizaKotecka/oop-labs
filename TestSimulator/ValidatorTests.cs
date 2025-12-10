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

    // Grupa 1: Testy sprawdzające poprawność formatowania (Trim + Wielka litera)
    [Theory]
    [InlineData("  shrek  ", 3, 10, '#', "Shrek")]
    [InlineData("shrek", 3, 10, '#', "Shrek")]
    [InlineData("   ", 3, 5, '?', "???")] // Same spacje -> trim do pustego -> padding
    public void Shortener_ShouldNormalizeString(string value, int min, int max, char placeholder, string expected)
    {
        var result = Validator.Shortener(value, min, max, placeholder);
        Assert.Equal(expected, result);
    }

    // Grupa 2: Testy sprawdzające ucinanie zbyt długich nazw
    [Theory]
    [InlineData("BardzoDlugaNazwa", 3, 6, '#', "Bardzo")]
    // Wyjaśnienie: Bierze 10 znaków ("a         "), robi TrimEnd -> zostaje "a",
    // "a" jest krótsze niż min (3), więc dodaje padding -> "A**"
    [InlineData("a                troll name", 3, 10, '*', "A**")]
    public void Shortener_ShouldTruncate_WhenTooLong(string value, int min, int max, char placeholder, string expected)
    {
        var result = Validator.Shortener(value, min, max, placeholder);
        Assert.Equal(expected, result);
    }

    // Grupa 3: Testy sprawdzające dopełnianie (padding) zbyt krótkich nazw
    [Theory]
    [InlineData("ab", 5, 10, '#', "Ab###")]
    [InlineData("x", 3, 5, '0', "X00")]
    public void Shortener_ShouldPad_WhenTooShort(string value, int min, int max, char placeholder, string expected)
    {
        var result = Validator.Shortener(value, min, max, placeholder);
        Assert.Equal(expected, result);
    }

    // Grupa 4: Testy przypadków brzegowych (null, empty, znaki specjalne)
    [Theory]
    [InlineData("", 3, 5, '*', "***")]
    [InlineData(null, 3, 5, '*', "***")]
    [InlineData("123", 5, 10, 'x', "123xx")] // Cyfry na początku nie są zmieniane na wielkie
    public void Shortener_ShouldHandleEdgeCases(string value, int min, int max, char placeholder, string expected)
    {
        var result = Validator.Shortener(value, min, max, placeholder);
        Assert.Equal(expected, result);
    }
}