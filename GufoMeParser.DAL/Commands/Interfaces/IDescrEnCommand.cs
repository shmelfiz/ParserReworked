namespace GufoMeParser.DAL.Commands.Interfaces
{
    public interface IDescrEnCommand : ICommand
    {
        void SendDataAsync(string word, string parsedTxt, string parsedHtml);
    }
}
