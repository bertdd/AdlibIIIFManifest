using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdlibIIIFManifest.Database
{
  public class Works
  {
    public async Task<WorkRecord> GetWork(int priref)
    {
      using (var client = new HttpClient())
      {
        var uri = new Uri(string.Format(Url, priref));
        var response = await client.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
          using (var stream = await response.Content.ReadAsStreamAsync())
          {
            return new WorkRecord(XDocument.Load(stream));
          }
        }
      }
      return null;
    }

    public string Url { get; set; } = "http://212.114.101.119/CIDDataSandbox/wwwopac.ashx?database=IIIFManifestWorks&search=priref={0}";
  }
}
