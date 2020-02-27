using System.Collections.Generic;
using System.Text;

namespace AdlibIIIFManifest.Models
{
  public class MetadataRecord
  {
    public MetadataRecord(int priref)
    {
      Priref = priref;
    }

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

    public int Priref { get; }

    public string Title { get; } = "Fire Raisers";

    public string Creator { get; } = "Michael Powell";

    public string Date { get; } = "1934-01-22";

    public string Genre { get; } = "Crime";

    public string Subject { get; } = "";

    public string CreateIdUrl(string manifestUrl) => string.Format(manifestUrl, Priref);


    public List<MetadataClass> MetaData =>
      new List<MetadataClass>
      {
        new MetadataClass { Label = "Director", Value = Creator },
        new MetadataClass { Label = "Date", Value = Date },
        new MetadataClass { Label = "Genre", Value = Genre },
        new MetadataClass { Label = "Subject", Value = Subject },
      };
  }
}