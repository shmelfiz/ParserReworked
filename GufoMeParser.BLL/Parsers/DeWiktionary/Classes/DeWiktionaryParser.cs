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

        public DeWiktionaryParser()
        {
            Container.InjectDependencies(this);
            _deWords = FileHelper.GetAllTextLinesFromFile(wordsFilePath);
        }

        public string GetNextUrl(string currentUrl = null)
        {
            //var  = 

            throw new NotImplementedException();
        }

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

        public void SendDataToDb(string currentWord, string parsedTxt, string parsedHtm)
        {
            throw new NotImplementedException();
        }
    }
}
