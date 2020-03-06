using System.Collections.Generic;

namespace AdlibIIIFManifest.Models
{
  public interface IMetaData
  {
    string CreateIdUrl(string manifestUrl);

    string Label { get; }

    string Description { get; }
    
    List<MetadataClass> MetaData { get; }
  }
}
