using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.BLL.Parsers.Parsers.DeWiktionary.Classes;
using GufoMeParser.BLL.Parsers.Parsers.GufoMe.Classes;
using GufoMeParser.BLL.Parsers.Parsers.Interfaces;
using GufoMeParser.BLL.ParsersFactory.Interfaces;
using GufoMeParser.Core;
using GufoMeParser.Core.Enum;
using GufoMeParser.Core.Helpers;
using GufoMeParser.Helpers;
using GufoMeParser.Infrastructure;
using GufoMeParser.Parsers.BLL.Parsers.EnAcademic.Classes;

namespace GufoMeParser
{
    class Program
    {
        private static IParserCreator _parserCreator { get; set; }
        private static IGufoVocabularyManager _vocabularyManager { get; set; }

        static void Main(string[] args)
        {
            InitializeDependencies();

            _parserCreator = Container.Resolve<IParserCreator>();

            InitializeParser(out IParser parser, out string mainUrl);

            RunParser(parser, mainUrl);
        }
        private static void InitializeDependencies()
        {
            Container.Initialize();
            MapperContainer.Initialize();
        }
         
        private static void InitializeParser(out IParser parser, out string mainUrl)
        {
            parser = null;
            mainUrl = string.Empty;

            Console.Write("Type parser name (\"Gufo\", \"EnAcademic\", \"DeWiktionary\"): ");
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
                        break;
                    }
                case ParserName.enacademic:
                    {
                        parser = _parserCreator.GetParser<EnAcademicParser>();
                        mainUrl = Defaults.EnAcademcMainUrl;
                        break;
                    }
                case ParserName.dewiktionary:
                    {
                        parser = _parserCreator.GetParser<DeWiktionaryParser>();
                        mainUrl = Defaults.DeWiktionaryMainUrl;
                        break;
                    }
            }           
        }

        private static void RunParser(IParser parser, string mainUrl)
        {
            var parsing = true;
            var urls = new List<string> { mainUrl };

            ConsoleWindowHelper.CheckWroteByUserLink(parser, urls);
            Console.WriteLine("Processing!");

            while (parsing)
            {
                parser.ParseData(urls.LastOrDefault());

                FileHelper.Save(parser.ParsedText, parser.ParsedPageName, ParsedDataType.ParsedTxt);
                FileHelper.Save(parser.ParsedHtml, parser.ParsedPageName, ParsedDataType.ParsedHtml);

                parser.SendDataToDb();

                var nextUrl = parser.GetNextUrl(urls.LastOrDefault());
                FileHelper.Save(nextUrl + "\n", "Links", ParsedDataType.ParsedLink).Wait();

                if(nextUrl.ToLower().Contains("complete"))
                {
                    parsing = false;
                }

                urls.Add(nextUrl);

                Console.WriteLine($"Current word is: \"{parser.ParsedPageName}\".");
                Console.CancelKeyPress += Cancel;

                Thread.Sleep(1000);
            }

            Console.WriteLine("Parsing is complete!");
            Console.WriteLine("Press any key for exit...");
            Console.ReadKey();
        }       

        private static async void Cancel(object sender, ConsoleCancelEventArgs args)
        {
            if(args.Cancel)
            {
                await Task.Factory.StartNew(() =>
                {
                    Environment.Exit(0);
                });
            }
        }
    }
}
