using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.DAL.Commands;
using GufoMeParser.DAL.Repository.Interfaces;

namespace GufoMeParser.BLL.Managers
{
    public class EnAcademicVocabularyManager : IEnAcademicVocabularyManager
    {
        private IRepository _repository { get; set; }

        public EnAcademicVocabularyManager()
        {
            Container.InjectDependencies(this);
        }

        public void SendData(string word, string parsedTxt, string parsedHtml)
        {
            if (string.IsNullOrEmpty(word) | string.IsNullOrEmpty(parsedTxt) | string.IsNullOrEmpty(parsedHtml))
            {
                return;
            }

            var command = new DescrRuCommand(_repository);
            command.SendDataAsync(word, parsedTxt, parsedHtml);
        }
    }
}
