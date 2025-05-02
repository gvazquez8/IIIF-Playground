using System.Text.Json;

namespace PlaygroundClient.DataModels;

public class ImageInfoDataModel
{
    public string context { get; set; }
    public string id { get; set; }
    public string type { get; set; }
    public string protocol { get; set; }
    public string profile { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public int maxWidth { get; set; }
    public int maxHeight { get; set; }
    public int maxArea { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, SourceGenerationContext.Default.ImageInfoDataModel);
    }
}
