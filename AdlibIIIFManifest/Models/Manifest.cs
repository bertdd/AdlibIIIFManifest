using Newtonsoft.Json;
using System.Collections.Generic;

namespace AdlibIIIFManifest.Models
{
  public class Manifest
  {
    [JsonProperty("@context")]
    public string Context { get; set; } = @"http://iiif.io/api/presentation/2/context.json";

    [JsonProperty("@type")]
    public string Type { get; set; } = "sc:Manifest";

    [JsonProperty("@id")]
    public string Id { get; set; } = "http://bk-img-data1:3000/works/150038592/manifest.json";

    [JsonProperty("label")]
    public string Label { get; set; } = "Fire Raisers(Michael Powell, 1934-01-22)";

    [JsonProperty("attribution")]
    public string Attribution { get; set; } = "BFI National Archive Special Collections";

    [JsonProperty("logo")]
    public string Logo { get; set; } = "https://upload.wikimedia.org/wikipedia/en/thumb/7/74/British_Film_Institute_logo_Black_and_White_solid.jpg/220px-British_Film_Institute_logo_Black_and_White_solid.jpg";

    [JsonProperty("related")]
    public RelatedClass Related = new RelatedClass
    {
      Id = "http://collections-search.bfi.org.uk/web/Details/ChoiceFilmWorks/150038592",
      Format = "application/html"
    };

    [JsonProperty("metadata")]
    public List<MetadataClass> MetaData = new List<MetadataClass>
    {
      new MetadataClass
      {
        Label = "Director",
        Value = "Powell, Michael"
      },

      new MetadataClass
      {
        Label = "Date",
        Value = "1934-01-22"
      },

      new MetadataClass
      {
        Label = "Genre",
        Value = "Crime"
      },

      new MetadataClass
      {
        Label =  "Subject",
      }
    };

    [JsonProperty("sequences")]
    public List<SequenceClass> Sequences = new List<SequenceClass>
    {
      new SequenceClass()
    };
  }
}
