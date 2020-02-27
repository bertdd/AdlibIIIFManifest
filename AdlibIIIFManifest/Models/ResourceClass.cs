using Newtonsoft.Json;

namespace AdlibIIIFManifest.Models
{
  public class ResourceClass
  {
    [JsonProperty("@type")]
    public string Type { get; set; }

    [JsonProperty("@id")]
    public string Id { get; set; }

    [JsonProperty("service")]
    public ServiceClass Service { get; set; }
  }
}