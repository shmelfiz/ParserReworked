using GufoMeParser.BLL.Parsers.Handlers.DeWiktionary.Interfaces;
using GufoMeParser.Core;
using GufoMeParser.Core.BuisinessModels;
using HtmlAgilityPack;
using System;
using System.Linq;

namespace GufoMeParser.BLL.Parsers.Handlers.DeWiktionary
{
    public class DeWiktionaryHtmlParsingHandler : IDeWiktionaryHtmlParsingHandler
    {
        #region Private properties

        private DeWiktionaryDataModel _wordParameters { get; set; }
        private HtmlDocument _webPage { get; set; }

        #endregion

        #region Constructor

        public DeWiktionaryHtmlParsingHandler(HtmlDocument webPage, DeWiktionaryDataModel wordParameters)
        {
            _wordParameters = wordParameters;
            _webPage = webPage;
        }

        #endregion

        #region Interface implementation

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

        #endregion

        #region Parsing Html

        private void FillPartOfSpeechHtml()
        {
            _wordParameters.PartOfSpeechSeit = _webPage.DocumentNode
                .SelectSingleNode(Defaults.DeWiktionaryPartOfSpeechXpath)?.ParentNode?.InnerHtml;
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
            _wordParameters.Example = _wordParameters.Example.Replace(Defaults.DeWiktionaryNewRowSymbol, string.Empty);
        }

        private void FillWordFormsHtml()
        {
            var xpath = Defaults.DeWiktionaryWordFormsXpath;
            var table = _webPage.DocumentNode.SelectSingleNode(xpath);
            var tBodyHtml = table?.ChildNodes.Where(tag => tag.Name.Equals(Defaults.DeWiktionaryWordFormsTag, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().InnerHtml.Replace(Defaults.DeWiktionaryNewRowSymbol, string.Empty);

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
