using AdlibIIIFManifest.Models;
using System.Web.Http;

namespace AdlibIIIFManifest.Controllers
{
  public class ValuesController : ApiController
  {
    // GET api/values/5
    public ManifestClass Get(int id)
    {
      return new ManifestClass(id);
    }
  }
}
