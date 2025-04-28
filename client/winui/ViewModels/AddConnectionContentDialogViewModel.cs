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
    private double _portNumber;

    [ObservableProperty]
    private bool _connected = false;

    private readonly ILoggingService _loggingService;
    private readonly IHttpClientFactory _httpClientFactory;

    public AddConnectionContentDialogViewModel(
        ILoggingService loggingService, 
        IHttpClientFactory httpClientFactory)
    {
        _loggingService = loggingService;
        _httpClientFactory = httpClientFactory;
    }
}
