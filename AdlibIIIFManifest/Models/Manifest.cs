using Newtonsoft.Json;

namespace AdlibIIIFManifest.Models
{
  public class Manifest
  {
    [JsonProperty("@Context")]
    public string Context { get; set; } = @"http://iiif.io/api/presentation/2/context.json";

    [JsonProperty("@type")]
    public string Type { get; set; } = "sc:Manifest";
  }
}