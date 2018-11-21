using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.BLL.Parsers.Parsers.Interfaces;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;
using GufoMeParser.Core.Helpers;
using GufoMeParser.Core;
using GufoMeParser.Core.BuisinessModels;
using GufoMeParser.BLL.Parsers.Helpers.DeWiktionary;
using GufoMeParser.BLL.Parsers.Handlers.DeWiktionary;

namespace GufoMeParser.BLL.Parsers.Parsers.DeWiktionary.Classes
{
    public class DeWiktionaryParser : IParser
    {
        private readonly string wordsFilePath = Directory.GetCurrentDirectory() + "\\DeWordsFile";

        private IDeWiktionaryVocabularyManager _vocabularyManager { get; set; }
        private List<string> _deWords { get; set; }
        private DeWiktionaryDataModel _wordParameters { get; set; }
        private int _wordId { get; set; } = 0;
        private bool _askAboutWordPos { get; set; } = true;

        public string ParsedPageName { get; private set; }
        public string ParsedText { get; private set; }
        public string ParsedHtml { get; private set; }

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
                _wordId = _askAboutWordPos ? DeWiktionaryHelper.GetWordIdForStart() : _wordId;
                _askAboutWordPos = false;

                _wordParameters = _vocabularyManager.GetWordParameters(_wordId) ?? new DeWiktionaryDataModel();
                ++_wordId;

                ParsedPageName = GetPageName(url);
                ParsedText = GetParsedTxt(url);
                ParsedHtml = GetParsedHtml(url);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error while parsing {url}! Error: \"{e}\"");
                return;
            }
        }

        public string GetNextUrl(string url = null)
        {
            var nextWordForReq = _vocabularyManager.GetWordForRequest(_wordId);

            if(string.IsNullOrEmpty(nextWordForReq))
            {
                Console.WriteLine($"Последнее id: {_wordId}.");
                return "complete";
            }

            var nextUrl = Defaults.DeWiktionaryStockUrl + nextWordForReq;

            return nextUrl;
        }

        public void SendDataToDb()
        {
            if(_wordParameters == null)
            {
                Console.WriteLine("Cant send data to db. WordParameters are empty!");
                return;
            }

            _wordParameters.Condition = 2;
            _vocabularyManager.SendData(_wordParameters);
        }

        #region Parse processing

        private string GetPageName(string url)
        {
            var pageName = DeWiktionaryHelper.GetWebPage(url)
                .GetElementbyId(Defaults.DeWiktionaryHeaderId)?.InnerText;

            return pageName ?? string.Empty;
        }

        private string GetParsedTxt(string url)
        {           
            var handler = new DeWiktionaryTxtParsingHandler(DeWiktionaryHelper.GetWebPage(url), _wordParameters);
            handler.FillWordParameters();

            var parsedText = $"Description: \"{_wordParameters.Description}\"; Example: \"{_wordParameters.Example}\"; " +
                $"PartOfSpeechSeit: \"{_wordParameters.PartOfSpeechSeit}\"; Transcription: \"{_wordParameters.Transcription}\"; WordForms: \"{_wordParameters.WordForms}\" \n";

            return parsedText;
        }

        private string GetParsedHtml(string url)
        {
            var wordParametersHtml = new DeWiktionaryDataModel();
            var handler = new DeWiktionaryHtmlParsingHandler(DeWiktionaryHelper.GetWebPage(url), wordParametersHtml);
            handler.FillWordParametersHtml();

            var parsedHtml = $"Description: \"{wordParametersHtml.Description}\"; Example: \"{wordParametersHtml.Example}\"; " +
                $"PartOfSpeechSeit: \"{wordParametersHtml.PartOfSpeechSeit}\"; Transcription: \"{wordParametersHtml.Transcription}\"; WordForms: \"{wordParametersHtml.WordForms}\" \n";

            return parsedHtml;
        }

        #endregion
    }
}
