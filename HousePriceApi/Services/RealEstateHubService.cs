using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class RealEstateHubService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "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";

    public RealEstateHubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> CallAIModelAsync(string modelUrl, object inputData)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, modelUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var jsonContent = JsonSerializer.Serialize(inputData);
        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
if (response.IsSuccessStatusCode)
{
    return await response.Content.ReadAsStringAsync();
}
// Asli error return karein taake pata chale kya masla hai
var errorContent = await response.Content.ReadAsStringAsync();
return "{\"error\": \"Failed: " + response.StatusCode + " - " + errorContent + "\"}";
    }
}