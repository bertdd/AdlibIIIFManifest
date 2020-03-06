using AdlibIIIFManifest.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdlibIIIFManifest.Models
{
  public class MetadataRecord : IMetaData
  {
    public MetadataRecord(int priref)
    {
      var works = new Works();
      var task = Task.Run(() => works.GetWork(priref));
      task.Wait();
      Work = task.Result;
    }

    public IMetaData Work { get; }

    public string Label => Work.Label;

    public List<MetadataClass> MetaData => Work.MetaData;

    public string Description => Work.Description;

    public string CreateIdUrl(string manifestUrl) => Work.CreateIdUrl(manifestUrl);
  }
}