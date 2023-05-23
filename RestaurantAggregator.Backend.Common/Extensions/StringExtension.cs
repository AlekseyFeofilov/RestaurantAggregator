namespace RestaurantAggregator.Backend.Common.Extensions;

public static class StringExtension
{
    private static readonly Random Random = new Random();

    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public static string GenerateRandomOrderNumber()
    {
        var randomString = "";
        var random = new Random();

        for (var i = 0; i < 3; i++) //todo make it in configuration
        {
            randomString += Chars[random.Next(Chars.Length)];
        }
        
        for (var i = 0; i < 8; i++)
        {
            randomString += random.Next(10);
        }

        return randomString;
    }
}