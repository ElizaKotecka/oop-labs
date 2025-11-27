namespace Simulator;
public class Animals
{
    private string _description = "Unknown";
    public uint Size { get; set; } = 3;

    public string Description
    {
        get => _description;
        init => _description = ValidateDesc(value);
    }

    public Animals()
    {
    }

    public Animals(string description)
    {
        Description = description;
    }
    private string ValidateDesc(string inputDesc)
    {
        string processedDesc = Validator.Shortener(inputDesc, 3, 15, '#');

        if (processedDesc.Length > 0 && char.IsLower(processedDesc[0]))
        {
            processedDesc = char.ToUpper(processedDesc[0]) + processedDesc.Substring(1);
        }

        return processedDesc;
    }
    public string Info => $"{Description} <{Size}>";
}
