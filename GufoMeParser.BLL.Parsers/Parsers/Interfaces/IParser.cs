namespace GufoMeParser.BLL.Parsers.Parsers.Interfaces
{
    public interface IParser
    {

        string ParsedPageName { get; }
        string ParsedText { get; }
        string ParsedHtml { get; }

        void ParseData(string url);
        string GetNextUrl(string currentUrl);
        void SendDataToDb();
    }
}
