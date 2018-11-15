using GufoMeParser.Core.BuisinessModels;
using GufoMeParser.Core.ModelsDTO;
using System.Collections.Generic;

namespace GufoMeParser.BLL.Managers.Interfaces
{
    public interface IDeWiktionaryVocabularyManager : IManager
    {
        List<DeWiktionaryDataModel> SendData(DeWiktionaryDataModelDTO dataModel);
        string GetWordForRequest();
    }
}
