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
        #region Private properties

        private DeWiktionaryDataModel _wordParameters { get; set; }
        private HtmlDocument _webPage { get; set; }

        #endregion

        #region Constructor

        public DeWiktionaryTxtParsingHandler(HtmlDocument webPage, DeWiktionaryDataModel wordParameters)
        {
            _wordParameters = wordParameters;
            _webPage = webPage;
        }

        #endregion

        #region Interface implementation

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

        #endregion

        #region Parsing word data

        private void FillPartOfSpeech()
        {
            _wordParameters.PartOfSpeechSeit = _webPage.DocumentNode
                .SelectSingleNode(Defaults.DeWiktionaryPartOfSpeechXpath)?
                .ParentNode?.InnerText;
        }

        private void FillTranscription()
        {
            _wordParameters.Transcription = _webPage.DocumentNode
                .SelectSingleNode(Defaults.DeWiktionaryTranscrXpath)?.InnerText;
        }

        private void FillDescription()
        {
            var xpath = Defaults.DeWiktionaryDescriptXpath;
            var elementForTxtParse = _webPage.DocumentNode.SelectSingleNode(xpath);

            if(elementForTxtParse == null)
            {
                _wordParameters.Description = string.Empty;
                return;
            }

            var childNodesWithTxt = elementForTxtParse.ChildNodes?.Where(node => node.Name.Equals("dd"));

            if (childNodesWithTxt.Count() == 0)
            {
                _wordParameters.Description = string.Empty;
                return;
            }

            _wordParameters.Description = childNodesWithTxt.Select(node => node.InnerText)
                .Aggregate((current, next) => current + "; " + next)
                .Replace("\n", string.Empty) ?? string.Empty;
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
            var childNodesWithTxt = elementForTxtParse.ChildNodes?.Where(node => node.Name.Equals("dd"));

            if(childNodesWithTxt.Count() == 0)
            {
                _wordParameters.Example = string.Empty;
                return;
            }

            _wordParameters.Example = childNodesWithTxt.Select(node => node.InnerText).Aggregate((current, next) => current + "; " + next)
                .Replace("\n", "") ?? string.Empty;
        }

        private void FillWordForms()
        {
            var xpath = Defaults.DeWiktionaryWordFormsXpath;
            var table = _webPage.DocumentNode.SelectSingleNode(xpath);
            var rows = table?.ChildNodes.Where(tag => tag.Name.Equals(Defaults.DeWiktionaryWordFormsTag, StringComparison.InvariantCultureIgnoreCase))
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

                var summedColumns = GetColumnsInnerTxt(columns).Replace("|\n", "").Replace(Defaults.DeWiktionaryNewRowSymbol, string.Empty).Replace(" |", " ");
                stringTableBuilder.AppendLine(summedColumns);
            }
            _wordParameters.WordForms = stringTableBuilder.ToString().Replace(Defaults.DeWiktionaryNewRowSymbol, string.Empty).Replace("\r", string.Empty);
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
            var allCollsData = columns.Select(column => column.ChildNodes?
            .Where(node => !string.IsNullOrEmpty(node.InnerText) && !node.InnerText.Equals(Defaults.DeWiktionaryNewRowSymbol))
            .Select(node => node.InnerText)).ToList();

            if(allCollsData.Count == 0)
            {
                return string.Empty;
            }

            var columnsTextBuilder = new StringBuilder();
            allCollsData.ForEach(column =>
            {
                var isItLastColumn = allCollsData.IndexOf(column) == allCollsData.Count - 1;

                var columnTxt = column
                .Aggregate((cur, next) => char.IsLetter(next[0]) ? cur + "|" + next : cur + next).Replace("&#32;", string.Empty);               
                columnsTextBuilder.AppendLine(!isItLastColumn ? columnTxt + "/" : columnTxt + ";");
            });

            return columnsTextBuilder.ToString();
        }

        #endregion
    }
}
