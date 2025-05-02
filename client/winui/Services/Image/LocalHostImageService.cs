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
public class LocalHostImageService : IImageService, IDisposable
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggingService _loggingService;
    public event EventHandler? OnConnectionEstablished;

    public Uri? ServerEndpoint => _serverEndpoint;
    private Uri? _serverEndpoint;

    private bool _connected = false;

    public LocalHostImageService(
        IHttpClientFactory httpClientFactory,
        ILoggingService loggingService)
    {
        _httpClientFactory = httpClientFactory;
        _loggingService = loggingService;
    }

    public async void Dispose()
    {
        await DisconnectAsync();
    }

    public Task<bool> ConnectAsync(string address)
    {
        _connected = true;
        _serverEndpoint = new Uri(address);
        _loggingService.Log($"Connected to server at {_serverEndpoint}");
        OnConnectionEstablished?.Invoke(this, EventArgs.Empty);
        return Task.FromResult(true);
    }


    public Task<bool> DisconnectAsync()
    {
        _connected = false;
        _serverEndpoint = null;
        return Task.FromResult(true);
    }

    private HttpClient GetServerHttpClient()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        client.BaseAddress = _serverEndpoint;
        return client;
    }

    public async Task<string> GetImageAsync(string imageUri)
    {
        if (!_connected)
        {
            _loggingService.LogError("Not connected to a server.");
            return string.Empty;
        }

        try
        {
            string value = await GetServerHttpClient().GetStringAsync(imageUri);
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
        if (!_connected)
        {
            _loggingService.LogError("Not connected to server.");
            return null;
        }

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
