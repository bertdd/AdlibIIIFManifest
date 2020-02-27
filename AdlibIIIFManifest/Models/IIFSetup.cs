namespace AdlibIIIFManifest.Models
{
  public class IIFSetup
  {
    public string LogoUrl { get; set; }
      = "https://upload.wikimedia.org/wikipedia/en/thumb/7/74/British_Film_Institute_logo_Black_and_White_solid.jpg/220px-British_Film_Institute_logo_Black_and_White_solid.jpg";

    public string Attribution { get; set; } = "BFI National Archive Special Collections";

    public string ManifestUrl { get; set; } = "http://bk-img-data1:3000/works/{0}/manifest";
  }
}