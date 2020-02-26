using Newtonsoft.Json;

namespace AdlibIIIFManifest.Models
{
  public class RelatedClass
  {
    [JsonProperty("@id")]
    public string Id { get; set; }

    [JsonProperty("format")]
    public string Format { get; set; }
  }
}