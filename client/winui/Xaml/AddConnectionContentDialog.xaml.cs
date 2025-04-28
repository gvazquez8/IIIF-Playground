using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PlaygroundClient.Services.Image;
using PlaygroundClient.Services.Logging;
using PlaygroundClient.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.NumberFormatting;

namespace PlaygroundClient.Xaml;

public enum ConnectionResult
{
    ConnectionEstablished,
    ConnectionFailed,
    Cancel,
    None
}
public sealed partial class AddConnectionContentDialog
{
    public ConnectionResult Result { get; private set; } = ConnectionResult.None;

    private AddConnectionContentDialogViewModel _viewModel;
    private IImageService _imageService;

    public AddConnectionContentDialog()
    {
        InitializeComponent();
        SetPortNumberFormatter();
        _viewModel = Ioc.Instance.GetService<AddConnectionContentDialogViewModel>();
        _imageService = Ioc.Instance.GetService<IImageService>();
    }

    private void SetPortNumberFormatter()
    {
        IncrementNumberRounder rounder = new();
        rounder.RoundingAlgorithm = RoundingAlgorithm.RoundDown;
        rounder.Increment = 1;

        DecimalFormatter formatter = new();
        formatter.FractionDigits = 0;
        formatter.NumberRounder = rounder;

        LocalServerPortNumberBox.NumberFormatter = formatter;
    }

    private void AddConnectionContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
    {
        Result = ConnectionResult.None;
    }

    private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        if (!_viewModel.ValidatePortRange(LocalServerPortNumberBox.Value))
        {
            Result = ConnectionResult.ConnectionFailed;
            args.Cancel = true; // cancel closing
            InvalidPortInfoBar.IsOpen = true;
            InvalidPortInfoBar.Message = "Port number is invalid.";
            return;
        }

        _viewModel.PortNumber = (int)LocalServerPortNumberBox.Value;

        ContentDialogButtonClickDeferral deferral = args.GetDeferral();
        bool connected = await _viewModel.ValidateServerRunningAsync();
        if (!connected)
        {
            Result = ConnectionResult.ConnectionFailed;
            args.Cancel = true;
            InvalidPortInfoBar.IsOpen = true;
            InvalidPortInfoBar.Message = $"Connection to local server on port {_viewModel.PortNumber} failed.";
        }
        else
        {
            await _imageService.ConnectAsync(_viewModel.IpAddress);
            InvalidPortInfoBar.IsOpen = false;
            Result = ConnectionResult.ConnectionEstablished;
        }
        deferral.Complete();
    }

    private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        Result = ConnectionResult.Cancel;
    }
}
