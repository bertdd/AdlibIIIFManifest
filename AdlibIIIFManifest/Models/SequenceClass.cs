using Newtonsoft.Json;
using System.Collections.Generic;

namespace AdlibIIIFManifest.Models
{
  public class SequenceClass
  {
    [JsonProperty("@type")]
    public string Type { get; set; } = "sc:Sequence";

    [JsonProperty("canvases")]
    public List<CanvasClass> Canvases { get; set; } = new List<CanvasClass>
    {
      new CanvasClass
      {
        Id = "http://bk-img-data1:3000/works150038592/SPD-992480/canvas/1",
        Label = "SPD-992480 - Photograph: film still",
        Width = 6000,
        Height = 8000
      }
    };
  }
}