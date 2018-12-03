using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.BLL.Parsers.Parsers.Interfaces;
using GufoMeParser.Core;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Text;

namespace GufoMeParser.BLL.Parsers.Parsers.GufoMe.Classes
{
    public class GufoParser : IParser
    {
        #region Private properties

        private IGufoVocabularyManager _vocabularyManager { get; set; }

        #endregion

        #region Public properties

        public string ParsedPageName { get; private set; } = string.Empty;
        public string ParsedText { get; private set; } = string.Empty;
        public string ParsedHtml { get; private set; } = string.Empty;

        #endregion

        #region Constructor

        public GufoParser()
        {
            Container.InjectDependencies(this);
        }

        #endregion

        #region Interface implementation

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
            if(currentUrl.Contains(Defaults.GuFoFinalWordCode))
            {
                return Defaults.SuccessFinalPhrase;
            }

            var parsedUrlDirty = GetWebPage(currentUrl)
                .DocumentNode.SelectNodes(Defaults.GeFoNextWordHrefXpath)
                .Select(x => x.Attributes.FirstOrDefault()).FirstOrDefault();

            var parsedUrl = new StringBuilder();
            parsedUrl.Append("\n");
            parsedUrl.Append("https://gufo.me");
            parsedUrl.Append(parsedUrlDirty.Value);

            return parsedUrl.ToString();
        }

        public void SendDataToDb()
        {
            if(string.IsNullOrEmpty(ParsedPageName) | string.IsNullOrEmpty(ParsedText) | string.IsNullOrEmpty(ParsedHtml))
            {
                return;
            }

            try
            {
                _vocabularyManager.SendData(ParsedPageName, ParsedText, ParsedHtml);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while sending data to db. Error: {e}.");
                return;
            }
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
                var parsedName = GetWebPage(url)
                     .DocumentNode.SelectNodes(Defaults.GuFoPageNameXpath)
                     .Select(x => x.InnerText).FirstOrDefault();

                return parsedName;
            }
            catch
            {
                throw new Exception("This ip was banned! Wait for 10 minutes and try again from last link in \"Links.txt\".");
            }
        }

        private string GetParsedTxt(string url)
        {
            var parsedTxtDirty = GetWebPage(url)
                .DocumentNode.SelectNodes(Defaults.GuFoTextForParseXpath)
                .Select(x => x.InnerText);

            var parsedText = new StringBuilder();

            foreach (var row in parsedTxtDirty)
            {
                parsedText.AppendLine(row);
            }

            return parsedText.ToString().Replace(Defaults.GuFoCopyRightText, string.Empty);
        }

        private string GetParsedHtml(string url)
        {
            var web = new HtmlWeb();
            var page = web.Load(url);
            var parsedHtmlBuilder = new StringBuilder();

            var parsedHtmlSplitted = page.DocumentNode
                .SelectNodes(Defaults.GuFoTextForParseXpath)
                .Select(x => x.OuterHtml);

            foreach (string parsedNode in parsedHtmlSplitted)
            {
                parsedHtmlBuilder.Append(parsedNode);
            }

            return parsedHtmlBuilder.ToString(); ;
        }

        #endregion
    }
}
