namespace GufoMeParser.BLL.Managers.Interfaces
{
    public interface IDeWiktionaryVocabularyManager : IManager
    {
        void SendData(string word, string parsedTxt, string parsedHtml);
    }
}
