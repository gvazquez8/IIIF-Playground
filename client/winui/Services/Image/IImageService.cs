using PlaygroundClientPlaygroundClient.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundClient.Services.Image;
public interface IImageService
{
    // Todo: how can I make this more secure?
    public Task<bool> ConnectAsync(string address);
    
    public event EventHandler? OnConnectionEstablished;
    
    public Task<bool> DisconnectAsync();

    public Task<string> GetImageAsync(string id);

    public Task<ImageCatalogue?> GetImageCatalogueAsync();

    public Uri? ServerEndpoint { get; }
}
