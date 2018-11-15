using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.BLL.Parsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using GufoMeParser.Core.Helpers;

namespace GufoMeParser.BLL.Parsers.DeWiktionary.Classes
{
    public class DeWiktionaryParser : IParser
    {
        private readonly string wordsFilePath = Directory.GetCurrentDirectory() + "\\DeWordsFile";

        private IDeWiktionaryVocabularyManager _vocabularyManager { get; set; }
        private List<string> _deWords { get; set; }
        private int _wordPosition { get; set; } = 0;

        public string ParsedPageName { get; set; }
        public string ParsedText { get; set; }
        public string ParsedHtml { get; set; }

        public DeWiktionaryParser()
        {
            Container.InjectDependencies(this);
            _deWords = FileHelper.GetAllTextLinesFromFile(wordsFilePath);
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

        public string GetNextUrl(string url)
        {
            //var  = 

            throw new NotImplementedException();
        }

        public void SendDataToDb()
        {
            throw new NotImplementedException();
        }

        #region Parse processing

        public string GetPageName(string url)
        {
            throw new NotImplementedException();
        }

        public string GetParsedHtml(string url)
        {
            throw new NotImplementedException();
        }

        public string GetParsedTxt(string url)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
