using PlaygroundClientPlaygroundClient.DataModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PlaygroundClientPlaygroundClient;

[JsonSourceGenerationOptions(
    WriteIndented = true)]
[JsonSerializable(typeof(ImageCatalogue))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
