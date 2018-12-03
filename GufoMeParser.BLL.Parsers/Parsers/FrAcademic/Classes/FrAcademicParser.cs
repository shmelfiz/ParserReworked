using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Parsers.Parsers.Interfaces;
using GufoMeParser.Core;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Text;

namespace GufoMeParser.BLL.Parsers.Parsers.FrAcademic.Classes
{
    public class FrAcademicParser : IParser
    {
        #region Public properties

        public string ParsedPageName { get; private set; }
        public string ParsedText { get; private set; }
        public string ParsedHtml { get; private set; }

        #endregion

        #region Constructor

        public FrAcademicParser()
        {
            Container.InjectDependencies(this);
        }

        #endregion

        #region Interface implementation

        public string GetNextUrl(string currentUrl)
        {
            if (currentUrl.Contains(Defaults.EnAcademicLastWordCode))
            {
                return Defaults.SuccessFinalPhrase;
            }

            var parsedUrlDirty = GetWebPage(currentUrl)
                .DocumentNode.SelectNodes(Defaults.EnAcademicNextWordHrefXpath)
                .Select(x => x.Attributes.FirstOrDefault()).FirstOrDefault();

            var parsedUrl = new StringBuilder();
            parsedUrl.Append(Defaults.FrAcademicNewRowSymbol);
            parsedUrl.Append(parsedUrlDirty.Value);

            return parsedUrl.ToString();
        }

        public void ParseData(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            try
            {
                ParsedPageName = GetPageName(url);
                ParsedText = string.Empty;// GetParsedTxt(url);
                ParsedHtml = string.Empty;// GetParsedHtml(url);
            }
            catch
            {
                Console.WriteLine($"Error while parsing {url}!");
                return;
            }
        }

        public void SendDataToDb()
        {
            return;
        }

        #endregion

        #region Parse processings

        private HtmlDocument GetWebPage(string url)
        {
            var web = new HtmlWeb();
            var page = web.Load(url);

            return page;
        }

        private string GetPageName(string url)
        {
            try
            {
                var parsedName = GetWebPage(url).DocumentNode
                    .SelectNodes(Defaults.EnAcademicPageNameXpath).Select(x => x.InnerText).FirstOrDefault();

                return parsedName;
            }
            catch
            {
                Console.WriteLine("This ip was banned! Wait for 10 minutes and try again from last link in \"Links.txt\".");
                return string.Empty;
            }
        }

        private string GetParsedTxt(string url)
        {
            var parsedTxtDirty = GetWebPage(url)
                .DocumentNode.SelectNodes(Defaults.EnAcademicXpathForDataParse)
                .Select(x => x.InnerText);

            var parsedText = new StringBuilder();

            foreach (var row in parsedTxtDirty)
            {
                parsedText.AppendLine(row);
            }

            return parsedText.ToString();
        }

        private string GetParsedHtml(string url)
        {
            var page = GetWebPage(url);
            var parsedHtml = new StringBuilder();

            var parsedHtmlSplitted = page.DocumentNode
                .SelectNodes(Defaults.EnAcademicXpathForDataParse)
                .Select(x => x.OuterHtml);

            foreach (string parsedNode in parsedHtmlSplitted)
            {
                parsedHtml.Append(parsedNode);
            }

            return parsedHtml.ToString(); ;
        }

        #endregion
    }
}
