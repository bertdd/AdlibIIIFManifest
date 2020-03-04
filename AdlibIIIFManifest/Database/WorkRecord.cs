using AdlibIIIFManifest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AdlibIIIFManifest.Database
{
  public class WorkRecord : IMetaData
  {
    public WorkRecord(XDocument xDocument)
    {
      Parse(xDocument);
    }

    void Parse(XDocument xDocument)
    {
      var adlibXml = xDocument.Element("adlibXML");
      var recordList = adlibXml.Element("recordList");
      var record = recordList.Element("record");
      Priref = int.Parse(record.Attribute("priref").Value);
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
              MetaData.Add(new MetadataClass { Label = "Date", Value = Date });
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
          MetaData.Add(new MetadataClass { Label = "Director", Value = Director });
          break;
        }
      }

      foreach (var contentGenre in record.Elements("Content_genre"))
      {
        var genre = contentGenre.Element("content.genre")?.Value;
        if (!string.IsNullOrWhiteSpace(genre))
        {
          MetaData.Add(new MetadataClass { Label = "Genre", Value = genre });
        }
      }

      foreach (var contentGenre in record.Elements("Content_subject"))
      {
        var subject = contentGenre.Element("content.subject")?.Value;
        if (!string.IsNullOrWhiteSpace(subject))
        {
          MetaData.Add(new MetadataClass { Label = "Subject", Value = subject });
        }
      }
    }

    public string CreateIdUrl(string manifestUrl) => string.Format(manifestUrl, Priref);
    
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
        sb.Append(Director);
        if (!string.IsNullOrWhiteSpace(Director) && !string.IsNullOrWhiteSpace(Date))
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

    bool UseBrackets => !string.IsNullOrWhiteSpace(Director) || !string.IsNullOrWhiteSpace(Date);


    public List<MetadataClass> MetaData { get; private set; } = new List<MetadataClass>();
    public int Priref { get; private set; }
    public string Title { get; private set; }
    public string Director { get; private set; }
    public string Date { get; private set; }
  }
}