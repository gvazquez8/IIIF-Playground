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

namespace PlaygroundClient.Xaml;

public sealed partial class Chrome : UserControl
{
    private ChromeViewModel _viewModel;
    public Chrome()
    {
        InitializeComponent();
        _viewModel = Ioc.Instance.GetService<ChromeViewModel>();
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
