namespace FindByRegex;

internal class Options
{
    public bool DisplayHelp { get; set; }
    public bool Recursive { get; set; }
    public bool SearchForFiles { get; set; }
    public bool SearchForDirectories { get; set; }
    public string? Pattern { get; set; }
    public string? Path { get; set; }
}