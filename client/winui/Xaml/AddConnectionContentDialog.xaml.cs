using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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

    public AddConnectionContentDialog()
    {
        InitializeComponent();
        _viewModel = Ioc.Instance.GetService<AddConnectionContentDialogViewModel>();

        SetPortNumberFormatter();
    }

    private void SetPortNumberFormatter()
    {
        LocalServerPortNumberBox.NumberFormatter = new DecimalFormatter
        {
            FractionDigits = 0,
        };
    }

    private void AddConnectionContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
    {
        Result = ConnectionResult.None;
    }

    private void AddConnectingContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
    {
        if (Result == ConnectionResult.ConnectionFailed)
        {
            args.Cancel = true; // cancel closing
        }
        else if (Result == ConnectionResult.ConnectionEstablished)
        {
            // save results AKA make a connection and store it for other services? (maybe factory)
            //_loggingService.Log($"Connection to local server on 127.0.0.1:{LocalServerPortNumberBox.Value} established.");
        }
    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        if (!string.IsNullOrEmpty(LocalServerPortNumberBox.Text))
        {
            Result = ConnectionResult.ConnectionEstablished;
        }
    }

    private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        Result = ConnectionResult.Cancel;
    }
}
