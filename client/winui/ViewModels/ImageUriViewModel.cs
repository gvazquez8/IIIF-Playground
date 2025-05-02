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

    [ObservableProperty, NotifyPropertyChangedFor("ImageUri")]
    private bool _enabled = false;

    public string ImageUri
    {
        get
        {
            if (!Enabled)
            {
                return "Not available";
            }

            StringBuilder sb = new();
            sb.AppendFormat("{0}{1}/{2}{3},{4},{5},{6}/{7},{8}/{9}{10}/{11}.{12}",
                HostUri!.ToString(),                            // 0
                ImageId,                                        // 1
                RegionAsPercent ? "pct:" : "",                  // 2
                RegionX, RegionY, RegionWidth, RegionHeight,    // 3,4,5,6
                SizeWidth, SizeHeight,                          // 7,8
                Mirror ? "!" : "", Rotation,                    // 9,10
                Quality, Format);                               // 11,12


            string imageUri = sb.ToString();
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
