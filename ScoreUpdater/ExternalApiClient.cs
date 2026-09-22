using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ScoreUpdater;

public class ExternalApiClient
{
    private readonly HttpClient _httpClient;

    //private readonly string _apiKey = "9bee09b876msh9752423ada93f3bp1c05a3jsnaf91f127f4cc";
    //private readonly string _apiHost = "tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com";

    public ExternalApiClient(HttpClient httpClient, IConfiguration config)
    {                       
        httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", config["ApiKeys:RapidApiHost"] ?? "");
        httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", config["ApiKeys:RapidApiKey"] ?? "");

        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? parameters = null)
    {
        try
        {
            var requestUrl = QueryHelpers.AddQueryString(
                url,
                (parameters ?? new())!
            );
            
            var response = await _httpClient.GetAsync(requestUrl);

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;            
        }
        catch (Exception ex) 
        {
            Console.Error.WriteLine(ex.Message);
        }

        return default;
    }
}
