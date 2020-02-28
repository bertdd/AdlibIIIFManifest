using AdlibIIIFManifest.Models;
using System.Web.Http;

namespace AdlibIIIFManifest.Controllers
{
  [RoutePrefix("")]
  public class ValuesController : ApiController
  {
    [Route("works/{priref}/manifest")]
    [HttpGet]
    public ManifestClass Works(int priref)
    {
      return new ManifestClass(priref);
    }
  }
}
