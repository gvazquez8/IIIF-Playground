using PlaygroundClient.Services.Image;
using PlaygroundClient.Services.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaygroundClient.ViewModels;
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ImageUriViewModel _imageUriViewModel;

    [ObservableProperty]
    private string _imageBase64;

    private readonly IImageService _imageService;
    private readonly ILoggingService _logging;

    public MainWindowViewModel(IImageService imageService, ILoggingService logging)
    {
        _imageService = imageService;
        _logging = logging;
        ImageUriViewModel = new ImageUriViewModel();
        GetImage();
    }

    private async void GetImage()
    {
        ImageBase64 = await _imageService.GetImage();
    }
}
