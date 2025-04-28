using CommunityToolkit.Mvvm.ComponentModel;
using PlaygroundClient.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundClient.ViewModels;
public partial class AddConnectionContentDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _connected = false;

    [ObservableProperty]
    private int _portNumber = 0;

    public string IpAddress => $"http://127.0.0.1:{PortNumber}";

    private readonly ILoggingService _loggingService;
    private readonly IHttpClientFactory _httpClientFactory;

    public AddConnectionContentDialogViewModel(
        ILoggingService loggingService, 
        IHttpClientFactory httpClientFactory)
    {
        _loggingService = loggingService;
        _httpClientFactory = httpClientFactory;
    }

    public bool ValidatePortRange(double port) => port < 0 || port > 65535 ? false : true;
    
    public async Task<bool> ValidateServerRunningAsync()
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync(IpAddress);
            if (response.IsSuccessStatusCode)
            {
                Connected = true;
                return true;
            }
        }
        catch (Exception ex)
        {
            _loggingService.LogError($"Error connecting to server: {ex.Message}");
        }
        Connected = false;
        return false;
    }
}
