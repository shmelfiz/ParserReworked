namespace GufoMeParser.BLL.Managers.Interfaces
{
    public interface IEnAcademicVocabularyManager : IManager
    {
        void SendData(string word, string parsedTxt, string parsedHtml);
    }
}
