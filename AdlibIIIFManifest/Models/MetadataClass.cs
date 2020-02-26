using Newtonsoft.Json;

namespace AdlibIIIFManifest.Models
{
  public class MetadataClass
  {
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
  }
}