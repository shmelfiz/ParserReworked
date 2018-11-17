using GufoMeParser.BLL.Parsers.Handlers.Interfaces;
using GufoMeParser.Core;
using GufoMeParser.Core.BuisinessModels;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Text;

namespace GufoMeParser.BLL.Parsers.Handlers.DeWiktionary
{
    public class DeWiktionaryHandler : IDeWiktionaryHandler
    {
        private DeWiktionaryDataModel _wordParameters { get; set; }
        private HtmlDocument _webPage { get; set; }

        public DeWiktionaryHandler(HtmlDocument webPage, DeWiktionaryDataModel wordParameters)
        {
            _wordParameters = wordParameters;
            _webPage = webPage;
        }

        public void FillWordParameters()
        {
            try
            {
                FillPartOfSpeech();
                FillTranscription();
                FillDescription();
                FillExamples();
                FillWordForms();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error while parsing word data. Error text: {e};");
            }
        }

        public void FillWordParametersHtml()
        {
            try
            {
                FillPartOfSpeechHtml();
                FillTranscriptionHtml();
                FillDescriptionHtml();
                FillExamplesHtml();
                FillWordFormsHtml();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while parsing word data html. Error text: {e};");
            }
        }

        #region Parsing word data

        private void FillPartOfSpeech()
        {
            _wordParameters.PartOfSpeechSeit = _webPage.DocumentNode
                .SelectSingleNode(Defaults.DeWiktionaryPartOfSpeechXpath)?.InnerText;
        }

        private void FillTranscription()
        {
            _wordParameters.Transcription = _webPage.DocumentNode
                .SelectSingleNode(Defaults.DeWiktionaryTranscrXpath)?.InnerText;
        }

        private void FillDescription()
        {
            var xpath = Defaults.DeWiktionaryDescriptXpath;
            _wordParameters.Description = _webPage.DocumentNode.SelectSingleNode(xpath)?.InnerText ?? string.Empty;
        }

        private void FillExamples()
        {
            var xpath = Defaults.DeWiktionaryExampleXpath;
            _wordParameters.Example = _webPage.DocumentNode.SelectSingleNode(xpath)?.InnerText ?? string.Empty;
            _wordParameters.Example = _wordParameters.Example.Replace("\n", "").Replace("“&#91;2&#93;", "");
        }

        private void FillWordForms()
        {
            var xpath = Defaults.DeWiktionaryWordFormsXpath;
            var table = _webPage.DocumentNode.SelectSingleNode(xpath);
            var rows = table?.ChildNodes.Where(tag => tag.Name.Equals("tbody", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().ChildNodes.Where(tag => tag.Name.Equals("tr", StringComparison.InvariantCultureIgnoreCase)).ToList();
            var stringTableBuilder = new StringBuilder();

            if(table == null)
            {
                _wordParameters.WordForms = string.Empty;
                return;
            }

            rows.RemoveAt(0);
            foreach (var row in rows)
            {
                var columns = row.ChildNodes.Where(tag => !tag.Name.Equals("th", StringComparison.InvariantCultureIgnoreCase) && !tag.Name.Equals("#text", StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (columns == null || columns.Count < 2)
                {
                    continue;
                }

                var summedColumns = $"{columns[0].InnerText}/{columns[1].InnerText};";
                stringTableBuilder.AppendLine(summedColumns);
            }
            _wordParameters.WordForms = stringTableBuilder.ToString().Replace("\n", "").Replace("\r", "");
        }

        #endregion

        #region Parsing Html

        private void FillPartOfSpeechHtml()
        {
            _wordParameters.PartOfSpeechSeit = _webPage.DocumentNode
                .SelectSingleNode(Defaults.DeWiktionaryPartOfSpeechXpath)?.InnerHtml;
        }

        private void FillTranscriptionHtml()
        {
            _wordParameters.Transcription = _webPage.DocumentNode
                .SelectSingleNode(Defaults.DeWiktionaryTranscrXpath)?.InnerHtml;
        }

        private void FillDescriptionHtml()
        {
            var xpath = Defaults.DeWiktionaryDescriptXpath;
            _wordParameters.Description = _webPage.DocumentNode.SelectSingleNode(xpath)?.InnerHtml ?? string.Empty;
        }

        private void FillExamplesHtml()
        {
            var xpath = Defaults.DeWiktionaryExampleXpath;
            _wordParameters.Example = _webPage.DocumentNode.SelectSingleNode(xpath)?.InnerHtml ?? string.Empty;
            _wordParameters.Example = _wordParameters.Example.Replace("\n", "");
        }

        private void FillWordFormsHtml()
        {
            var xpath = Defaults.DeWiktionaryWordFormsXpath;
            var table = _webPage.DocumentNode.SelectSingleNode(xpath);
            var tBodyHtml = table?.ChildNodes.Where(tag => tag.Name.Equals("tbody", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().InnerHtml.Replace("\n", "");

            if (table == null)
            {
                _wordParameters.WordForms = string.Empty;
                return;
            }

            _wordParameters.WordForms = tBodyHtml;
        }

        #endregion
    }
}
