using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using PlaygroundClient.Services.Logging;

namespace PlaygroundClient.Services.Image;
public class AshIIIFImageAPIEndpoint : IImageService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggingService _loggingService;

    public AshIIIFImageAPIEndpoint(
        IHttpClientFactory httpClientFactory,
        ILoggingService loggingService)
    {
        _httpClientFactory = httpClientFactory;
        _loggingService = loggingService;
    }

    public async Task<string> GetImage()
    {
        using HttpClient client = _httpClientFactory.CreateClient();

        try
        {
            string value = await client.GetStringAsync("http://127.0.0.1:5000/pexels-osmanarabaciart-19640755/full/500,800/30/default.jpg");
            return value;
        }
        catch (Exception ex)
        {
            _loggingService.LogError(ex.Message);
            return string.Empty;
        }

    }
}
