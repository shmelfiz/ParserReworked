using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.DAL.Commands;
using GufoMeParser.DAL.Repository.Interfaces;

namespace GufoMeParser.BLL.Managers
{
    public class FrAcademicVocabularyManager : IFrAcademicVocabularyManager
    {
        private IRepository _repository { get; set; }

        public FrAcademicVocabularyManager()
        {
            Container.InjectDependencies(this);
        }

        public void SendData(string word)
        {
            if(_repository == null || string.IsNullOrEmpty(word))
            {
                return;
            }

            var command = new FrAcademicAddWordCommand(_repository);
            command.SendDataAsync(word);
        }
    }
}
