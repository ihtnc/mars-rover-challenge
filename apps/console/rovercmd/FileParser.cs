using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Apps.Console;

public class FileParser
{
    private readonly IFileContentProvider _fileContentProvider;

    public FileParser() : this(new FileContentProvider()) { }
    public FileParser(IFileContentProvider fileContentProvider)
    {
        _fileContentProvider = fileContentProvider;
    }

    public Input Parse(string filePath)
    {
        var content = _fileContentProvider.ReadAllText(filePath);
        var input = InputHelper.ParseInput(content);
        return input;
    }
}