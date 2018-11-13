using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.BLL.Parsers.GufoMe.Classes;
using GufoMeParser.BLL.Parsers.Interfaces;
using GufoMeParser.BLL.ParsersFactory.Interfaces;
using GufoMeParser.Core;
using GufoMeParser.Core.Enum;
using GufoMeParser.Core.Helpers;
using GufoMeParser.Helpers;
using GufoMeParser.Parsers.BLL.EnAcademic.Classes;

namespace GufoMeParser
{
    class Program
    {
        private static IParserCreator _parserCreator{ get; set;}
        private static IGufoVocabularyManager _vocabularyManager { get; set; }

        static void Main(string[] args)
        {
            InitializeIoC();

            InitializeParser(out IParser parser, out string mainUrl);

            RunParser(parser, mainUrl);
        }
        private static void InitializeIoC()
        {
            _parserCreator = Container.Resolve<IParserCreator>();
            _vocabularyManager = Container.Resolve<IGufoVocabularyManager>();
        }
         
        private static void InitializeParser(out IParser parser, out string mainUrl)
        {
            parser = null;
            mainUrl = string.Empty;

            Console.Write("Type \"Gufo\" or \"EnAcademic\": ");
            var nameOfParser = Console.ReadLine().ToLower();

            var isNameParsed = Enum.TryParse(nameOfParser, out ParserName parserName);

            if (!isNameParsed)
            {
                throw new Exception("Typed name of name of parser is not support!");
            }

            switch (parserName)
            {
                case ParserName.gufo:
                    {
                        parser = _parserCreator.GetParser<GufoParser>();
                        mainUrl = Defaults.GuFoMainUrl;
                        return;
                    }
                case ParserName.enacademic:
                    {
                        parser = _parserCreator.GetParser<EnAcademicParser>();
                        mainUrl = Defaults.EnAcademcMainUrl;
                        return;
                    }
            }           
        }

        private static void RunParser(IParser parser, string mainUrl)
        {
            var parsing = true;
            var wordsCount = 0L;
            var urls = new List<string> { mainUrl };

            ConsoleWindowHelper.CheckWroteByUserLink(parser, urls);

            Console.WriteLine("Processing!");

            while (parsing)
            {
                wordsCount++;
                var currentWord = parser.GetPageName(urls.LastOrDefault());
                var parsedTxt = parser.GetParsedTxt(urls.LastOrDefault());
                var parsedHtml = parser.GetParsedHtml(urls.LastOrDefault());

                FileHelper.Save(parsedTxt, currentWord, ParsedDataType.ParsedTxt).Wait();
                FileHelper.Save(parsedHtml, currentWord, ParsedDataType.ParsedHtml).Wait();

                parser.SendDataToDb(currentWord, parsedTxt, parsedHtml);

                var nextUrl = parser.GetNextUrl(urls.LastOrDefault());
                FileHelper.Save(nextUrl, "Links", ParsedDataType.ParsedLink).Wait();

                if(nextUrl.ToLower().Contains("complete"))
                {
                    parsing = false;
                }

                urls.Add(nextUrl);

                Console.WriteLine(currentWord);
                Console.CancelKeyPress += Cancel;

                Thread.Sleep(1000);
            }
        }       

        private static void Cancel(object sender, ConsoleCancelEventArgs args)
        {
            if(args.Cancel)
            {
                Environment.Exit(0);
            }
        }
    }
}
