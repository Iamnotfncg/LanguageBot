using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
public static class ApiWordService
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task<string> GetRandomWordAsync(string languageCode)
    {
        var url = $"https://random-word-api.herokuapp.com/word?lang={languageCode}";
        var response = await httpClient.GetStringAsync(url);
        var words = JsonConvert.DeserializeObject<List<string>>(response);

        return words?.FirstOrDefault() ?? "No word found";
    }
}