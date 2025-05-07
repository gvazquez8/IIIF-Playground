using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlaygroundClient.DataModels;
public class ImageCatalogue
{
    public ObservableCollection<string>? ImageIds { get; set; }
}
