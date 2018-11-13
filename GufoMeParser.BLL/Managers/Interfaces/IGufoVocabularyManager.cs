namespace GufoMeParser.BLL.Managers.Interfaces
{
    public interface IGufoVocabularyManager : IManager
    {
        void SendData(string word, string parsedTxt, string parsedHtml);
    }
}
