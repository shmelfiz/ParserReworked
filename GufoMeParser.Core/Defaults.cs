namespace GufoMeParser.Core
{
    public class Defaults
    {
        public const string GuFoMainUrl = "https://gufo.me/dict/ozhegov/а";
        public const string GuFoStockUrl = "https://gufo.me/dict/ozhegov/";
        public const string GuFoFinalWordCode = "%D1%8F%D1%89%D1%83%D1%80";
        public const string GuFoNextWordHrefXpath = "//i[@class='fa fa-long-arrow-right']//preceding-sibling::a";
        public const string GuFoTextForParseXpath = "//p";
        public const string GuFoCopyRightText = "&copy; 2018 Gufo.me";
        public const string GuFoPageNameXpath = "//h1";

        public const string EnAcademicMainUrl = "http://terms_en.enacademic.com/2205";
        public const string EnAcademicStockUrl = "http://terms_en.enacademic.com/";
        public const string EnAcademicLastWordCode = "47432";
        public const string EnAcademicNextWordHrefXpath = "//li[@class='next']/a";
        public const string EnAcademicXpathForDataParse = "//dd";
        public const string EnAcademicPageNameXpath = "//dt";

        public const string FrAcademicMainUrl = "http://encyclopedie_universelle.fracademic.com/23238";
        public const string FrAcademicStockUrl = "http://encyclopedie_universelle.fracademic.com/";
        public const string FrAcademicNewRowSymbol = "\n";

        public const string DeWiktionaryStockUrl = "https://de.wiktionary.org/wiki/";
        public const string DeWiktionaryMainUrl = "https://de.wiktionary.org/wiki/Abt";
        public const string DeWiktionaryHeaderId = "firstHeading";
        public const string DeWiktionaryPartOfSpeechXpath = "//a[@title='Hilfe:Wortart']";
        public const string DeWiktionaryTranscrXpath = "//span[@class=\"ipa\"]";
        public const string DeWiktionaryDescriptXpath = "//div[@class='mw-parser-output']//p[text() = 'Bedeutungen:']/following-sibling::dl[1]";
        public const string DeWiktionaryExampleXpath = "//div[@class='mw-parser-output']//p[text() = 'Beispiele:']/following-sibling::dl[1]";
        public const string DeWiktionarySupsForDeleteXpath = "//div[@class='mw-parser-output']//p[text() = 'Beispiele:']/following-sibling::dl[1]//sup";
        public const string DeWiktionaryWordFormsXpath = "//div[@class='mw-parser-output']//h3/following-sibling::table";
        public const string DeWiktionaryFileDirectoryName = "\\DeWordsFile";
        public const string DeWiktionaryNewRowSymbol = "\n";
        public const string DeWiktionaryWordFormsTag = "tbody";
        public const string DeWiktionaryBigLetterFileName = "_BIG";
        public const string DeWiktionarySmallLetterFileName = "_SMALL";

        public const string SuccessFinalPhrase = "complete";

        public const string SavedFilesFolderName = "\\SavedFiles";
        public const string SavedHtmlFilesFolderName = "\\SavedFilesHtml";
        public const string SavedLinksFilesFolderName = "\\LinksFile";
    }
}
