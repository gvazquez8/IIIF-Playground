using PlaygroundClientPlaygroundClient.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundClient.Services.Image;
public interface IImageService
{
    public Task<string> GetImageAsync(string id);
    public Task<ImageCatalogue?> GetImageCatalogueAsync();
}
