using System.Text;
using Newtonsoft.Json;

namespace RestaurantAggregator.Backend.API.Extensions;

public static class HttpContextAccessorExtension
{
    public static async Task<T> ReadRequestBodyAsync<T>(this IHttpContextAccessor contextAccessor)
    {
        string bodyStr;

        // Allows using several time the stream in ASP.Net Core
        var request = contextAccessor.HttpContext!.Request;
        request.EnableBuffering(); 

        // Arguments: Stream, Encoding, detect encoding, buffer size 
        // AND, the most important: keep stream opened
        using (var reader 
               = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
        {
            bodyStr = await reader.ReadToEndAsync();
        }

        // Rewind, so the core is not lost when it looks at the body for the request
        request.Body.Position = 0;
        
        return JsonConvert.DeserializeObject<T>(bodyStr) ?? throw new InvalidOperationException();
    }
}