using AdlibIIIFManifest.Models;
using System.Web.Http;
using System.Web;

namespace AdlibIIIFManifest.Controllers
{
  [RoutePrefix("")]
  public class ValuesController : ApiController
  {
    [Route("works/{priref}/manifest")]
    [HttpGet]
    public ManifestClass Works(int priref)
    {
      HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
      return new ManifestClass(priref);
    }
  }
}
