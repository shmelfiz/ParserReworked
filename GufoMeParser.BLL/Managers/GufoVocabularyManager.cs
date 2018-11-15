using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.DAL.Commands;
using GufoMeParser.DAL.Repository.Interfaces;

namespace GufoMeParser.BLL.Managers
{
    public class GufoVocabularyManager : IGufoVocabularyManager
    {
        private IRepository _repiository { get; set; }
        public GufoVocabularyManager()
        {
            Container.InjectDependencies(this);
        }

        public void SendData(string word, string parsedTxt, string parsedHtml)
        {
            if (string.IsNullOrEmpty(word) | string.IsNullOrEmpty(parsedTxt) | string.IsNullOrEmpty(parsedHtml))
            {
                return;
            }

            var command = new DescrEnCommand(_repiository);
            command.SendDataAsync(word, parsedTxt, parsedHtml);
        }
    }
}
