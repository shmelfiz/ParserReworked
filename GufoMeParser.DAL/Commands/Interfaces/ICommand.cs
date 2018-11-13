namespace GufoMeParser.DAL.Commands.Interfaces
{
    public interface ICommand
    {
        void SendDataAsync(string word, string parsedTxt, string parsedHtml);
    }
}
