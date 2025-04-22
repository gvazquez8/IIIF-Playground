using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PlaygroundClient.Services.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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

    private ILoggingService _loggingService;

    public AddConnectionContentDialog()
    {
        InitializeComponent();
        _loggingService = Ioc.Instance.GetService<ILoggingService>();
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
            _loggingService.Log($"Connection to {ServerUrlTextBox.Text} established.");
        }
    }

    private async void SubmitButton_Click(object sender, RoutedEventArgs args)
    {
        if (!string.IsNullOrEmpty(ServerUrlTextBox.Text))
        {
            Result = ConnectionResult.ConnectionEstablished;
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs args)
    {
        Result = ConnectionResult.Cancel;
    }
}
