using Newtonsoft.Json;
using System.Collections.Generic;

namespace AdlibIIIFManifest.Models
{
  public class ManifestClass
  {
    public ManifestClass(int priref)
    {
      var record = new MetadataRecord(priref);
      CreateManifest(record);
    }

    void CreateManifest(IMetaData record)
    {
      Id = record.CreateIdUrl(setup.ManifestUrl); 
      Label = record.Label;
      Description = record.Description;
      Attribution = setup.Attribution;
      Logo = setup.LogoUrl;

      if (!string.IsNullOrWhiteSpace(setup.AISUrl))
      {
        Related = new RelatedClass
        {
          Id = record.CreateIdUrl(setup.AISUrl),
          Format = "application/html"
        };
      }

      MetaData = record.MetaData;

      Sequences = new List<SequenceClass>
      {
        new SequenceClass
        {
          Canvases = new List<CanvasClass>
          {
            new CanvasClass
            {
              Type = "sc:canvas",
              Id = "http://bk-img-data1:3000/works150038592/SPD-992480/canvas/1",
              Label = "SPD-992480 - Photograph: film still",
              Width = 6000,
              Height = 8000,
              Images = new List<ImageClass>
              {
                new ImageClass
                {
                  Type = "oa:Annotation",
                  Motivation = "sc:painting",
                  On = "http://bk-img-data1:3000/works150038592/SPD-992480/canvas/1",
                  Resource = new ResourceClass
                  {
                    Type = "dctypes:Image",
                    Id = "http://bk-ci-web:8182/iiif/2/0x060a2b340101010201010f121374bcc2383dd102728305801c56645106EF7E9C/full/full/0/default.jpg",
                    Service = new ServiceClass
                    {
                      Id = "http://bk-ci-web:8182/iiif/2/0x060a2b340101010201010f121374bcc2383dd102728305801c56645106EF7E9C",
                      Profile = "http://iiif.io/api/image/2/level2.json"
                    }
                  }
                }
              }
            }
          }
        }
      };
    }

    [JsonProperty("@context")]
    public string Context { get; } = "http://iiif.io/api/presentation/2/context.json";

    [JsonProperty("@type")]
    public string Type { get; } = "sc:Manifest";

    [JsonProperty("@id")]
    public string Id { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("attribution")]
    public string Attribution { get; set; }

    [JsonProperty("logo")]
    public string Logo { get; set; }

    [JsonProperty("related")]
    public RelatedClass Related { get; set; }

    [JsonProperty("metadata")]
    public List<MetadataClass> MetaData { get; set; }

    [JsonProperty("sequences")]
    public List<SequenceClass> Sequences { get; set; }

    readonly IIFSetup setup = new IIFSetup();
  }
}
