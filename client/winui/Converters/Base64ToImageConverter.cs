using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundClient.Converters;
public class Base64ToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string base64String)
        {
            byte[] bytes = System.Convert.FromBase64String(base64String);
            BitmapImage image = new();
            using (MemoryStream stream = new(bytes))
            {
                image.SetSource(stream.AsRandomAccessStream());
            }
            return image;
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}