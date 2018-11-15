namespace GufoMeParser.BLL.Parsers.Interfaces
{
    public interface IParser
    {

        string ParsedPageName { get; set; }
        string ParsedText { get; set; }
        string ParsedHtml { get; set; }

        void ParseData(string url);
        string GetNextUrl(string currentUrl);
        void SendDataToDb();
    }
}
