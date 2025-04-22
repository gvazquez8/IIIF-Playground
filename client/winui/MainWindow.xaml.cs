using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using PlaygroundClient.Xaml;
namespace PlaygroundClient;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OnConnectMenuFlyoutItemClick(object sender, RoutedEventArgs e)
    {
        AddConnectionContentDialog.XamlRoot = RootGrid.XamlRoot;
        await AddConnectionContentDialog.ShowAsync();
    }

    private void OnDisconnectMenuFlyoutItemClick(object sender, RoutedEventArgs e)
    {

    }
}
