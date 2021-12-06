using MarsRoverChallenge.Send;
using MarsRoverChallenge.Send.Models;
using CommandLine;

// See https://aka.ms/new-console-template for more information
Parser.Default
    .ParseArguments<MarsRoverChallenge.Apps.Console.Options>(args)
    .WithParsed(Run)
    .WithNotParsed(HandleParseError);

static void Run(MarsRoverChallenge.Apps.Console.Options opts)
{
    var parser = new MarsRoverChallenge.Apps.Console.FileParser();
    var input = parser.Parse(opts.InputFile);
    var processor = new Processor();
    var output = processor.Run(input);
    HandleOutput(output);
}

static void HandleParseError(IEnumerable<Error> errs)
{
    Console.WriteLine("Invalid arguments.");
}

static void HandleOutput(Output output)
{
    foreach(var o in output.RoverOutputs)
    {
        Console.WriteLine(o.Location);
    }
}