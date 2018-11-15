using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.BLL.Parsers.Interfaces;
using GufoMeParser.Core;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Text;

namespace GufoMeParser.Parsers.BLL.EnAcademic.Classes
{
    public class EnAcademicParser : IParser
    {
        private IEnAcademicVocabularyManager _vocabularyManager { get; set; }

        public string ParsedPageName { get; set; }
        public string ParsedText { get; set; }
        public string ParsedHtml { get; set; }

        public EnAcademicParser()
        {
            Container.InjectDependencies(this);
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
                ParsedText = GetParsedTxt(url);
                ParsedHtml = GetParsedHtml(url);
            }
            catch
            {
                Console.WriteLine($"Error while parsing {url}!");
                return;
            }
        }

        public string GetNextUrl(string currentUrl)
        {
            if (currentUrl.Contains(Defaults.EnAcademicLastWordCode))
            {
                return "Complete!";  //костылец
            }

            var parsedUrlDirty = GetWebPage(currentUrl)
                .DocumentNode.SelectNodes("//li[@class='next']/a")
                .Select(x => x.Attributes.FirstOrDefault()).FirstOrDefault();

            var parsedUrl = new StringBuilder();
            parsedUrl.Append("\n");
            parsedUrl.Append(parsedUrlDirty.Value);

            return parsedUrl.ToString();
        }

        public void SendDataToDb()
        {
            if (string.IsNullOrEmpty(ParsedPageName) | string.IsNullOrEmpty(ParsedText) | string.IsNullOrEmpty(ParsedHtml))
            {
                return;
            }

            try
            {
                _vocabularyManager.SendData(ParsedPageName, ParsedText, ParsedHtml);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error while sending data to db. Error: {e}.");
                return;
            }
        }

        #region Parse processings

        private string GetParsedTxt(string url)
        {
            var parsedTxtDirty = GetWebPage(url)
                //.DocumentNode.SelectNodes("//meta[@name='Description']")
                .DocumentNode.SelectNodes("//dd")
                //.Select(x => x.GetAttributeValue("Content", "false"));
                .Select(x => x.InnerText);

            var parsedText = new StringBuilder();

            foreach (var row in parsedTxtDirty)
            {
                parsedText.AppendLine(row);
            }

            return parsedText.ToString();
        }

        private string GetPageName(string url)
        {
            try
            {
                var parsedName = GetWebPage(url).DocumentNode
                    .SelectNodes("//dt").Select(x => x.InnerText).FirstOrDefault();

                return parsedName;
            }
            catch
            {
                Console.WriteLine("This ip was banned! Wait for 10 minutes and try again from last link in \"Links.txt\".");
                return string.Empty;
            }
        }

        private string GetParsedHtml(string url)
        {
            var web = new HtmlWeb();
            var page = web.Load(url);
            var parsedHtml = new StringBuilder();

            var parsedHtmlSplitted = page.DocumentNode
                .SelectNodes("//dd")
                .Select(x => x.OuterHtml);

            foreach (string parsedNode in parsedHtmlSplitted)
            {
                parsedHtml.Append(parsedNode);
            }

            return parsedHtml.ToString(); ;
        }

        private HtmlDocument GetWebPage(string url)
        {
            var web = new HtmlWeb();
            var page = web.Load(url);

            return page;
        }

        #endregion
    }
}
