using HtmlAgilityPack;
using System;

namespace GufoMeParser.BLL.Parsers.Helpers.DeWiktionary
{
    public static class DeWiktionaryHelper
    {
        public static HtmlDocument GetWebPage(string url)
        {
            var web = new HtmlWeb();
            var page = web.Load(url);

            return page;
        }

        public static int GetWordIdForStart(int currentId)
        {
            Console.WriteLine("Do you wanna start parsing from first word in dict_de ?");
            Console.WriteLine("Type \"y\" or \"n\":");
            var answer = Console.ReadLine();
            var repeatQuestion = true;
            var resultWordId = 0;

            while (repeatQuestion)
            {
                if (!string.IsNullOrEmpty(answer) && answer.Contains("y"))
                {
                    Console.WriteLine("Programm starts from first word!");
                    break;
                }

                if (!string.IsNullOrEmpty(answer) && answer.Contains("n"))
                {
                    Console.WriteLine("Type word id for start:");

                    if (int.TryParse(Console.ReadLine(), out int typedWordId))
                    {
                        resultWordId = typedWordId;
                        break;
                    }
                }
            }
            return resultWordId;
        }
    }
}
