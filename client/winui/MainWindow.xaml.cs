using PlaygroundClient.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace PlaygroundClient;

public sealed partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = Ioc.Instance.GetService<MainWindowViewModel>();
    }

    private BitmapImage? ConvertToBitmap(string? base64String)
    {
        if (string.IsNullOrWhiteSpace(base64String))
        {
            return null;
        }

        byte[] bytes = Convert.FromBase64String(base64String);
        BitmapImage image = new();
        using (MemoryStream stream = new(bytes))
        {
            image.SetSource(stream.AsRandomAccessStream());
        }
        return image;
    }


}
