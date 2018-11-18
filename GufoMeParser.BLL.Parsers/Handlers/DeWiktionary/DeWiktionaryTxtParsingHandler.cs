using GufoMeParser.BLL.Parsers.Handlers.DeWiktionary.Interfaces;
using GufoMeParser.Core;
using GufoMeParser.Core.BuisinessModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GufoMeParser.BLL.Parsers.Handlers.DeWiktionary
{
    public class DeWiktionaryTxtParsingHandler : IDeWiktionaryTxtParsingHandler
    {
        private DeWiktionaryDataModel _wordParameters { get; set; }
        private HtmlDocument _webPage { get; set; }

        public DeWiktionaryTxtParsingHandler(HtmlDocument webPage, DeWiktionaryDataModel wordParameters)
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
            var elementForTxtParse = _webPage.DocumentNode.SelectSingleNode(xpath);

            if (elementForTxtParse == null)
            {
                _wordParameters.Example = string.Empty;
                return;
            }

            RemoveSupsFromExample(elementForTxtParse);
            _wordParameters.Example = elementForTxtParse.InnerText ?? string.Empty;
            _wordParameters.Example = _wordParameters.Example.Replace("\n", "");
        }

        private void FillWordForms()
        {
            var xpath = Defaults.DeWiktionaryWordFormsXpath;
            var table = _webPage.DocumentNode.SelectSingleNode(xpath);
            var rows = table?.ChildNodes.Where(tag => tag.Name.Equals("tbody", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().ChildNodes.Where(tag => tag.Name.Equals("tr", StringComparison.InvariantCultureIgnoreCase)).ToList();
            var stringTableBuilder = new StringBuilder();

            if (table == null)
            {
                _wordParameters.WordForms = string.Empty;
                return;
            }

            rows.RemoveAt(0);
            foreach (var row in rows)
            {
                var columns = row.ChildNodes.Where(tag => !tag.Name.Equals("th", StringComparison.InvariantCultureIgnoreCase) 
                    && !tag.Name.Equals("#text", StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (columns == null || columns.Count < 2)
                {
                    continue;
                }

                var summedColumns = GetColumnsInnerTxt(columns).Replace("|\n", "").Replace("\n", "").Replace(" |", " ");
                stringTableBuilder.AppendLine(summedColumns);
            }
            _wordParameters.WordForms = stringTableBuilder.ToString().Replace("\n", "").Replace("\r", "");
        }

        #endregion

        #region Accessories

        private void RemoveSupsFromExample(HtmlNode exampleMainNode)
        {
            exampleMainNode.ChildNodes?.Where(node => node.Name.Equals("dd", StringComparison.InvariantCultureIgnoreCase))?.ToList().ForEach(node =>
            {
                node?.ChildNodes?.Where(childNode => childNode.Name.Equals("sup", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Remove();
            });
        }

        private string GetColumnsInnerTxt(List<HtmlNode> columns)
        {
            var leftColumnNodesTxt = columns[0]
                .ChildNodes?.Where(node => !string.IsNullOrEmpty(node.InnerText) && !node.InnerText.Equals("\n")).Select(node => node.InnerText);
            var rightColumnNodesTxt = columns[1]
                .ChildNodes?.Where(node => !string.IsNullOrEmpty(node.InnerText) && !node.InnerText.Equals("\n")).Select(node => node.InnerText);

            if(leftColumnNodesTxt.Count() == 0 | rightColumnNodesTxt.Count() == 0)
            {
                return string.Empty;
            }

            var leftColumnTxt = leftColumnNodesTxt
                .Aggregate((cur, next) => char.IsLetter(next[0]) ? cur + "|" + next : cur + next);
            var rightColumnTxt = rightColumnNodesTxt
                .Aggregate((cur, next) => char.IsLetter(next[0]) ? cur + "|" + next : cur + next);

            if (string.IsNullOrEmpty(leftColumnTxt) | string.IsNullOrEmpty(rightColumnTxt))
            {
                return string.Empty;
            }

            var summedColumns = $"{leftColumnTxt}/{rightColumnTxt};";

            return summedColumns;
        }

        #endregion
    }
}
