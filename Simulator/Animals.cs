namespace Simulator;
public class Animals
{
    private string _description = "Unknown";

    public string Description
    {
        get => _description;
        init => _description = ValidateDesc(value);
    }
    public uint Size { get; set; } = 3;

    public Animals()
    {

    }

    public Animals(string description)
    {
        Description = description;
    }
    private string ValidateDesc(string inputDesc)
    {
        string processedDesc = (inputDesc ?? "").Trim();

        if (processedDesc.Length > 15)
        {
            processedDesc = processedDesc.Substring(0, 15).TrimEnd();
        }

        if (processedDesc.Length < 3)
        {
            processedDesc = processedDesc.PadRight(3, '#');
        }

        if (processedDesc.Length > 0 && char.IsLower(processedDesc[0]))
        {
            processedDesc = char.ToUpper(processedDesc[0]) + processedDesc.Substring(1);
        }

        return processedDesc;
    }
    public string Info => $"{Description} <{Size}>";
}
