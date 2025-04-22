using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using PlaygroundClient.ViewModels;
using System.Threading.Tasks;

namespace PlaygroundClient.Xaml;

public sealed partial class Chrome : UserControl
{
    private ChromeViewModel _viewModel;
    public Chrome()
    {
        InitializeComponent();
        _viewModel = Ioc.Instance.GetService<ChromeViewModel>();
    }

    private BitmapImage ConvertToBitmap(string? base64String)
    {
        BitmapImage image = new();
        if (string.IsNullOrWhiteSpace(base64String))
        {
            // The "no image" placeholder
            image.UriSource = new Uri("ms-appx:///Assets/StoreLogo.png");
            return image;
        }

        byte[] bytes = Convert.FromBase64String(base64String);
        using (MemoryStream stream = new(bytes))
        {
            image.SetSource(stream.AsRandomAccessStream());
        }
        return image;
    }

    private async void EditRegionButton_Click(object sender, RoutedEventArgs e)
    {
        EditRegionParameterContentDialog.XamlRoot = this.XamlRoot;
        await EditRegionParameterContentDialog.ShowAsync();
    }

    private void EditSizeButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void EditRotationButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
