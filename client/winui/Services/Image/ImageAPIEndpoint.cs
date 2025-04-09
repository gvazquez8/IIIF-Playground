using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using PlaygroundClient.Services.Logging;
using PlaygroundClientPlaygroundClient.DataModels;
using PlaygroundClientPlaygroundClient;

namespace PlaygroundClient.Services.Image;
public class ImageAPIEndpoint : IImageService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggingService _loggingService;

    private const string localhost = "http://127.0.0.1:5000";
    private Uri serverEndpoint;

    public ImageAPIEndpoint(
        IHttpClientFactory httpClientFactory,
        ILoggingService loggingService)
    {
        _httpClientFactory = httpClientFactory;
        _loggingService = loggingService;

        serverEndpoint = new Uri(localhost);
    }

    private HttpClient GetServerHttpClient()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        client.BaseAddress = serverEndpoint;
        return client;
    }

    public async Task<string> GetImageAsync(string id)
    {
        try
        {
            string value = await GetServerHttpClient().GetStringAsync($"{id}/full/500,800/30/default.jpg");
            return value;
        }
        catch (Exception ex)
        {
            _loggingService.LogError(ex.Message);
            return string.Empty;
        }
    }

    public async Task<ImageCatalogue?> GetImageCatalogueAsync()
    {
        try
        {
            HttpResponseMessage result = await GetServerHttpClient().GetAsync("catalogue");
            if (result.IsSuccessStatusCode)
            {
                string catalogue = await result.Content.ReadAsStringAsync();
                ImageCatalogue? fetchedCatalogue = JsonSerializer.Deserialize(catalogue, SourceGenerationContext.Default.ImageCatalogue);
                return fetchedCatalogue;
            }
            else
            {
                _loggingService.LogError($"Failed to fetch image catalogue: {result.ReasonPhrase}");
                return null;
            }
        }
        catch (Exception ex)
        {
            _loggingService.LogError(ex.Message);
            return null;
        }
    }
}
