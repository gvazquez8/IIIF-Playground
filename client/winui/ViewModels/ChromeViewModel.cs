using PlaygroundClient.Services.Image;
using PlaygroundClient.Services.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;
using PlaygroundClient.DataModels;
using CommunityToolkit.Mvvm.Messaging;
using PlaygroundClient.Messages;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace PlaygroundClient.ViewModels;
public partial class ChromeViewModel : ObservableRecipient, IRecipient<ImageUriParametersChangedMessage>
{
    [ObservableProperty]
    private ImageUriViewModel _imageUriViewModel;

    [ObservableProperty]
    private string? _imageBase64;

    [ObservableProperty]
    private ImageInfoDataModel? _imageInfo;

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

    private async void OnImageServiceConnected(object? sender, EventArgs e)
    {
        await InitializeImageCatalogueAsync();
        ImageUriViewModel.HostUri = _imageService.ServerEndpoint;
        ImageUriViewModel.ImageId = ImageCatalogue?.ImageIds?.First() ?? "ERROR";
        ImageUriViewModel.Enabled = true;
    }

    private async Task InitializeImageCatalogueAsync() => ImageCatalogue = await _imageService.GetImageCatalogueAsync();

    private async void RequestImage(string imageUri)
    {
        ImageBase64 = await _imageService.GetImageAsync(imageUri);
        ImageInfo = await _imageService.GetImageInfoAsync(ImageUriViewModel.ImageId);

        if (ImageInfo == null)
        {
            _logging.LogError("ImageInfo is null");
            return;
        }

        ImageUriViewModel.RegionWidth = ImageInfo.maxWidth;
        ImageUriViewModel.RegionHeight = ImageInfo.maxHeight;
    }

    public void Receive(ImageUriParametersChangedMessage message) => RequestImage(message.Value);
}
