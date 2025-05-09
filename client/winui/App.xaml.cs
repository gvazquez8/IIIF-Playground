﻿using Microsoft.UI.Xaml;

namespace PlaygroundClient;
public partial class App : Application
{
    private Window m_window;

    public App()
    {
        InitializeComponent();
        Ioc.Instance.Initialize();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }

}
