using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.DAL.Commands;

namespace GufoMeParser.BLL.Managers
{
    public class GufoVocabularyManager : IGufoVocabularyManager
    {
        public void SendData(string word, string parsedTxt, string parsedHtml)
        {
            if (string.IsNullOrEmpty(word) | string.IsNullOrEmpty(parsedTxt) | string.IsNullOrEmpty(parsedHtml))
            {
                return;
            }

            var command = new DescrEnCommand();
            command.SendDataAsync(word, parsedTxt, parsedHtml);
        }
    }
}
