using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PlaygroundClient.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundClient.ViewModels;
public partial class ImageUriViewModel : ObservableObject
{
    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private int _regionX;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private int _regionY;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private int _regionWidth;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private int _regionHeight;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private bool _regionAsPercent;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private int _sizeWidth;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private int _sizeHeight;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private bool _sizeAsPercent;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private int _rotation;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private bool _mirror;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private string _quality;
    
    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private string _format;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private string _imageId;

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private Uri? _hostUri;

    public string ImageUri
    {
        get
        {
            string imageUri = $"{HostUri?.ToString() ?? "ERROR"}{(RegionAsPercent ? "pct:" : "")}{RegionX},{RegionY},{RegionWidth},{RegionHeight}/{SizeWidth},{SizeHeight}/{Rotation}{(Mirror ? "!" : "")}/{Quality}.{Format}";

            WeakReferenceMessenger.Default.Send(new ImageUriParametersChangedMessage(imageUri));
            return imageUri;
        }
    }

    public ImageUriViewModel()
    {
        // Initialize properties with default values if needed
        RegionX = 0;
        RegionY = 0;
        RegionWidth = 100;
        RegionHeight = 100;
        SizeWidth = 1000;
        SizeHeight = 1000;
        Rotation = 0;
        Mirror = false;
        Quality = "default";
        Format = "jpg";
    }
}
