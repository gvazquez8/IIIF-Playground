using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using PlaygroundClient.Xaml;
using PlaygroundClient.Services.Image;
namespace PlaygroundClient;

public sealed partial class MainWindow : Window
{
    private readonly IImageService _imageService;
    public MainWindow()
    {
        InitializeComponent();
        _imageService = Ioc.Instance.GetService<IImageService>();
    }

    private async void OnConnectMenuFlyoutItemClick(object sender, RoutedEventArgs e)
    {
        AddConnectionContentDialog.XamlRoot = RootGrid.XamlRoot;
        await AddConnectionContentDialog.ShowAsync();
        
        if (AddConnectionContentDialog.Result == ConnectionResult.ConnectionEstablished && _imageService.ServerEndpoint != null)
        {
            ConnectionStatusTextBlock.Text = $"Connected to {_imageService.ServerEndpoint.Authority}";
        }
    }

    private async void OnDisconnectMenuFlyoutItemClick(object sender, RoutedEventArgs e)
    {
        await _imageService.DisconnectAsync();
        ConnectionStatusTextBlock.Text = "Disconnected";
    }
}
