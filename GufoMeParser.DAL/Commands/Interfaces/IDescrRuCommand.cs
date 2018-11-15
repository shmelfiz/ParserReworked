namespace GufoMeParser.DAL.Commands.Interfaces
{
    public interface IDescrRuCommand : ICommand
    {
        void SendDataAsync(string word, string parsedTxt, string parsedHtml);
    }
}
