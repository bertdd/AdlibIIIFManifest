using Newtonsoft.Json;

namespace AdlibIIIFManifest.Models
{
  public class ImageClass
  {
    [JsonProperty("@type")]
    public string Type { get; set; }

    [JsonProperty("motivation")]
    public string Motivation { get; set; }

    [JsonProperty("on")]
    public string On { get; set; }

    [JsonProperty("resource")]
    public ResourceClass Resource { get; set; }
  }
}