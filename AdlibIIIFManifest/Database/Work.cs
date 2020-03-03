using System.Xml.Linq;

namespace AdlibIIIFManifest.Database
{
  public class Work
  {
    public Work(XDocument xDocument)
    {
      Parse(xDocument);
    }

    void Parse(XDocument xDocument)
    {
      var adlibXml = xDocument.Element("adlibXML");
      var recordList = adlibXml.Element("recordList");
      var record = recordList.Element("record");
      var titlegroup = record.Element("Title");
      var title = titlegroup.Element("title")?.Value;
      var article = titlegroup.Element("title.article")?.Value;
      Title = string.IsNullOrWhiteSpace(article) ? title : $"{article} {title}";
      foreach (var titleDate in record.Elements("Title_date"))
      {
        var titleDateType = titleDate.Element("title_date.type");
        if (titleDateType != null)
        {
          foreach (var value in titleDateType.Elements("value"))
          {
            var lang = value.Attribute("lang")?.Value;
            var type = value?.Value;
            if (lang == "neutral" && type == "03_R")
            {
              Date = titleDate.Element("title_date_start")?.Value;
              break;
            }
          }
        }
      }

      var credits = record.Elements("credits");
      foreach (var credit in credits)
      {
        var creditType = credit.Element("credit.type")?.Value;
        if (creditType == "Director")
        {
          Director = credit.Element("credit.name")?.Value;
          break;
        }
      }
    }

    public string Title { get; private set; }
    public string Director { get; private set; }
    public string Date { get; private set; }
  }
}