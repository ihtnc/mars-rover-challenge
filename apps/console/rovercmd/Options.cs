using CommandLine;

namespace MarsRoverChallenge.Apps.Console;

public class Options
{
    [Option('i', "input", Required = true, HelpText = "Path to the input file.")]
    public string InputFile { get; set; } = string.Empty;
}