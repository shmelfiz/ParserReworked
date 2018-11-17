namespace GufoMeParser.Core
{
    public class Defaults
    {
        public const string GuFoMainUrl = "https://gufo.me/dict/ozhegov/а";
        public const string GuFoStockUrl = "https://gufo.me/dict/ozhegov/";
        public const string GuFoFinalWordCode = "%D1%8F%D1%89%D1%83%D1%80";

        public const string EnAcademcMainUrl = "http://terms_en.enacademic.com/2205";
        public const string EnAcademcStockUrl = "http://terms_en.enacademic.com/";
        public const string EnAcademicLastWordCode = "47432";

        public const string DeWiktionaryStockUrl = "https://de.wiktionary.org/wiki/";
        public const string DeWiktionaryMainUrl = "https://de.wiktionary.org/wiki/Abt";
        public const string DeWiktionaryHeaderId = "firstHeading";
        public const string DeWiktionaryPartOfSpeechXpath = "//a[@title='Hilfe:Wortart']";
        public const string DeWiktionaryTranscrXpath = "//span[@class=\"ipa\"]";
        public const string DeWiktionaryDescriptXpath = "//div[@class='mw-parser-output']//p[text() = 'Bedeutungen:']/following-sibling::dl[1]";
        public const string DeWiktionaryExampleXpath = "//div[@class='mw-parser-output']//p[text() = 'Beispiele:']/following-sibling::dl[1]";
        public const string DeWiktionaryWordFormsXpath = "//div[@class='mw-parser-output']//h3/following-sibling::table";
    }
}
