namespace GufoMeParser.BLL.Parsers.Interfaces
{
    public interface IParser
    {

        string GetParsedTxt(string url);
        string GetNextUrl(string currentUrl);
        string GetPageName(string url);
        string GetParsedHtml(string url);
        void SendDataToDb(string currentWord, string parsedTxt, string parsedHtm);
    }
}
