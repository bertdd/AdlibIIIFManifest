using Newtonsoft.Json;
using System.Collections.Generic;

namespace AdlibIIIFManifest.Models
{
  public class SequenceClass
  {
    [JsonProperty("@type")]
    public string Type { get; } = "sc:Sequence";

    [JsonProperty("canvases")]
    public List<CanvasClass> Canvases { get; set; }
  }
}