namespace MarsRoverChallenge.Send.Utilities;

public static class StringHelper
{
    private static readonly long _baseline;

    static StringHelper()
    {
        var utc = new DateTime(2019,1,4);
        _baseline = new DateTimeOffset(utc, TimeSpan.Zero).Ticks;
    }

    public static string GenerateShortId()
    {
        var current = DateTime.UtcNow.Ticks;
        var value = current - _baseline;
        return value.ToString("x");
    }
}