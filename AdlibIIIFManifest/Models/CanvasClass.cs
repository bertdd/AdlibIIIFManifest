using Newtonsoft.Json;
using System.Collections.Generic;

namespace AdlibIIIFManifest.Models
{
  public class CanvasClass
  {
    [JsonProperty("@type")]
    public string Type { get; set; }

    [JsonProperty("@id")]
    public string Id { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("images")]
    public List<ImageClass> Images { get; set; }
  }   
}