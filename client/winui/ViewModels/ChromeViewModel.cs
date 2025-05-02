using PlaygroundClient.Services.Image;
using PlaygroundClient.Services.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;
using PlaygroundClientPlaygroundClient.DataModels;
using CommunityToolkit.Mvvm.Messaging;
using PlaygroundClient.Messages;

namespace PlaygroundClient.ViewModels;
public partial class ChromeViewModel : ObservableRecipient, IRecipient<ImageUriParametersChangedMessage>
{
    [ObservableProperty]
    private ImageUriViewModel _imageUriViewModel;

    [ObservableProperty]
    private string? _imageBase64;

    [ObservableProperty]
    private ImageCatalogue? _imageCatalogue;

    private readonly IImageService _imageService;
    private readonly ILoggingService _logging;

    public ChromeViewModel(IImageService imageService, ILoggingService logging)
    {
        _imageService = imageService;
        _imageService.OnConnectionEstablished += OnImageServiceConnected;
        _logging = logging;
        ImageUriViewModel = new ImageUriViewModel();
        WeakReferenceMessenger.Default.Register<ImageUriParametersChangedMessage>(this);
    }

    private void OnImageServiceConnected(object? sender, EventArgs e)
    {
        ImageUriViewModel.HostUri = _imageService.ServerEndpoint;
        InitializeImageCatalogue();
    }

    private async void InitializeImageCatalogue() => ImageCatalogue = await _imageService.GetImageCatalogueAsync();

    private async void RequestImage(string imageUri)
    {
        ImageBase64 = await _imageService.GetImageAsync(imageUri);
    }

    public void Receive(ImageUriParametersChangedMessage message) => RequestImage(message.Value);
}
