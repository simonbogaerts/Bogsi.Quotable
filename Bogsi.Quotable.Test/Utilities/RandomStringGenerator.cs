namespace Bogsi.Quotable.Test.Utilities;

public sealed class RandomStringGenerator
{
    public static string GenerateRandomString(int length)
    {
        return string.Join("", Enumerable.Repeat(0, length).Select(n => (char)new Random().Next(32, 127)));
    }
}
