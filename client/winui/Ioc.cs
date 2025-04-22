using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaygroundClient.Services.Image;
using PlaygroundClient.Services.Logging;
using PlaygroundClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace PlaygroundClient;
class Ioc
{
    public static Ioc Instance { get; } = new Ioc();
    public T GetService<T>() => _serviceProvider.GetService<T>();

    private ServiceCollection _serviceCollection;
    private ServiceProvider _serviceProvider;

    private bool _initialized = false;

    private Ioc()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (_initialized)
            return;

        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddSingleton<ILoggingService, DebugLogger>();
        _serviceCollection.AddSingleton<IImageService, LocalHostImageService>();
        _serviceCollection.AddHttpClient();

        _serviceCollection.AddSingleton<ChromeViewModel>();
        _serviceProvider = _serviceCollection.BuildServiceProvider();

        _initialized = true;
    }
}
