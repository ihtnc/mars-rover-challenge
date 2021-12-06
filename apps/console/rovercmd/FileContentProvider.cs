namespace MarsRoverChallenge.Apps.Console;

public interface IFileContentProvider
{
    string ReadAllText(string path);
}

public class FileContentProvider : IFileContentProvider
{
    public string ReadAllText(string path) => File.ReadAllText(path);
}