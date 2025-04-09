using PlaygroundClient.Services.Image;
using PlaygroundClient.Services.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;
using PlaygroundClientPlaygroundClient.DataModels;

namespace PlaygroundClient.ViewModels;
public partial class ChromeViewModel : ObservableObject
{
    [ObservableProperty]
    private ImageUriViewModel _imageUriViewModel;

    [ObservableProperty]
    private string _imageBase64;

    private readonly IImageService _imageService;
    private readonly ILoggingService _logging;

    public ChromeViewModel(IImageService imageService, ILoggingService logging)
    {
        _imageService = imageService;
        _logging = logging;
        ImageUriViewModel = new ImageUriViewModel();
        GetImage();
    }

    private async void GetImage()
    {
        //ImageCatalogue catalogue = await _imageService.GetImageCatalogueAsync();
        //ImageBase64 = await _imageService.GetImageAsync(catalogue.ImageIds[0]);
    }
}
