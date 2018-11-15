using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.BLL.Parsers.Interfaces;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Text;

namespace GufoMeParser.Parsers.BLL.EnAcademic.Classes
{
    public class EnAcademicParser : IParser
    {
        private IEnAcademicVocabularyManager _vocabularyManager { get; set; }

        public EnAcademicParser()
        {
            Container.InjectDependencies(this);
        }

        public string GetParsedTxt(string url)
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

        public string GetNextUrl(string currentUrl)
        {
            if (currentUrl.Contains("47432"))
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

        public string GetPageName(string url)
        {
            try
            {
                var parsedName = GetWebPage(url).DocumentNode
                    .SelectNodes("//dt").Select(x => x.InnerText).FirstOrDefault();

                return parsedName;
            }
            catch
            {
                throw new Exception("This ip was banned! Wait for 10 minutes and try again from last link in \"Links.txt\".");
            }
        }

        public string GetParsedHtml(string url)
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

        public void SendDataToDb(string currentWord, string parsedTxt, string parsedHtml)
        {
            if (string.IsNullOrEmpty(currentWord) | string.IsNullOrEmpty(parsedTxt) | string.IsNullOrEmpty(parsedHtml))
            {
                return;
            }

            try
            {
                _vocabularyManager.SendData(currentWord, parsedTxt, parsedHtml);
            }
            catch
            {
                return;
            }
        }

        private HtmlDocument GetWebPage(string url)
        {
            var web = new HtmlWeb();
            var page = web.Load(url);

            return page;
        }
    }
}
