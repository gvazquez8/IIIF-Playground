using PlaygroundClient.DataModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PlaygroundClient;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    NumberHandling = JsonNumberHandling.WriteAsString | JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(ImageCatalogue))]
[JsonSerializable(typeof(ImageInfoDataModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
