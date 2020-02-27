using System.Text;

namespace AdlibIIIFManifest.Models
{
  public class MetadataRecord
  {
    public MetadataRecord(int priref)
    {
      Priref = priref;
    }

    public int Priref { get; }

    public string Title { get; } = "Fire Raisers";

    public string Creator { get; } = "Michael Powell";

    public string Date { get; } = "1934-01-22";

    public string Id(string manifestUrl) => string.Format(manifestUrl, Priref);

    public string Label
    {
      get
      {
        var sb = new StringBuilder();
        sb.Append(Title);
        if (UseBrackets)
        {
          sb.Append(" (");
        }
        sb.Append(Creator);
        if (!string.IsNullOrWhiteSpace(Creator) && !string.IsNullOrWhiteSpace(Date))
        {
          sb.Append(", ");
          sb.Append(Date);
        }
        if (UseBrackets)
        {
          sb.Append(")");
        }
        return sb.ToString();
      }
    }

    bool UseBrackets => !string.IsNullOrWhiteSpace(Creator) || !string.IsNullOrWhiteSpace(Date);
  }
}