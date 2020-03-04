using Newtonsoft.Json;

namespace AdlibIIIFManifest.Models
{
  public class ServiceClass
  {
    [JsonProperty("@context")]
    public string Context {get; set;} = "http://iiif.io/api/image/2/context.json";

    [JsonProperty("@id")]
    public string Id { get; set; }

    [JsonProperty("profile")]
    public string Profile { get; set; }
  }
}