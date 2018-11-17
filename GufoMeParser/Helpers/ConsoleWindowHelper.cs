using GufoMeParser.BLL.Parsers.Parsers.Interfaces;
using GufoMeParser.Core;
using GufoMeParser.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GufoMeParser.Helpers
{
    public static class ConsoleWindowHelper
    {
        public static void CheckWroteByUserLink(IParser parser, List<string> urls)
        {
            Console.WriteLine("If u wanna start from main url, type \"continue\".");
            Console.WriteLine("For the exit press \"Ctrl + C\".");

            var nameOfParser = parser.GetType().Name.Replace("Parser", "").ToLower();
            var isNameParsed = Enum.TryParse(nameOfParser, out ParserName parserName);

            if (!isNameParsed)
            {
                throw new Exception("Typed name of name of parser is not support!");
            }

            switch (parserName)
            {
                case ParserName.gufo:
                    {
                        CheckWroteByUserLinkGufo(urls);
                        return;
                    }
                case ParserName.enacademic:
                    {
                        CheckWroteByUserLinkEnAcademic(urls);
                        return;
                    }
                case ParserName.dewiktionary:
                    {
                        CheckWroteByUserLinkDeWiktionary(urls);
                        return;
                    }
            }
        }

        private static void CheckWroteByUserLinkGufo(List<string> urls)
        {
            var isRight = false;

            while (isRight == false)
            {
                Console.Write("Insert start url (example: https://gufo.me/dict/ozhegov/а): ");
                var readStartLink = Console.ReadLine();
                urls.Add(readStartLink);

                if (urls.Last().ToLower().Contains("continue"))
                {
                    Console.WriteLine(string.Format("Programm starting from start url: {0}", Defaults.GuFoMainUrl));
                    urls.RemoveAt(urls.IndexOf(urls.Last()));
                }

                if (urls.Count > 1 && !urls.Last().Contains(Defaults.GuFoStockUrl))
                {
                    Console.WriteLine(string.Format("The link must be like: {0}", Defaults.GuFoMainUrl));
                    urls.RemoveAt(urls.IndexOf(urls.Last()));

                    continue;
                }

                isRight = true;
            }
        }

        private static void CheckWroteByUserLinkEnAcademic(List<string> urls)
        {
            var isRight = false;

            while (isRight == false)
            {
                Console.Write("Insert start url (example: http://terms_en.enacademic.com/2205): ");
                var readStartLink = Console.ReadLine();
                urls.Add(readStartLink);

                if (urls.Last().ToLower().Contains("continue"))
                {
                    Console.WriteLine(string.Format("Programm starting from start url: {0}", Defaults.EnAcademcMainUrl));
                    urls.RemoveAt(urls.IndexOf(urls.Last()));
                }

                if (urls.Count > 1 && !urls.Last().Contains(Defaults.EnAcademcStockUrl))
                {
                    Console.WriteLine(string.Format("The link must be like: {0}", Defaults.EnAcademcMainUrl));
                    urls.RemoveAt(urls.IndexOf(urls.Last()));

                    continue;
                }

                isRight = true;
            }
        }

        private static void CheckWroteByUserLinkDeWiktionary(List<string> urls)
        {
            var isRight = false;

            while (isRight == false)
            {
                Console.Write("Insert start url (example: https://de.wiktionary.org/wiki/Abt): ");
                var readStartLink = Console.ReadLine();
                urls.Add(readStartLink.Trim());

                if (urls.Last().ToLower().Contains("continue"))
                {
                    Console.WriteLine(string.Format("Programm starting from start url: {0}", Defaults.DeWiktionaryMainUrl));
                    urls.RemoveAt(urls.IndexOf(urls.Last()));
                    isRight = true;

                    continue;
                }

                if (urls.Count > 1 && !urls.Last().Contains(Defaults.DeWiktionaryStockUrl))
                {
                    Console.WriteLine(string.Format("The link must be like: {0}", Defaults.DeWiktionaryMainUrl));
                    urls.RemoveAt(urls.IndexOf(urls.Last()));

                    continue;
                }

                isRight = true;
            }
        }
    }
}
