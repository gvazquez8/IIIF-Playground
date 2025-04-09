using CommunityToolkit.Mvvm.ComponentModel;
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

    public string ImageUri => $"https://example.com/iiif/{(RegionAsPercent ? "pct:" : "")}{RegionX},{RegionY},{RegionWidth},{RegionHeight}/{SizeWidth},{SizeHeight}/{Rotation}{(Mirror ? "!" : "")}/{Quality}.{Format}";

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
