using GufoMeParser.Core.BuisinessModels;
using System.Collections.Generic;

namespace GufoMeParser.BLL.Managers.Interfaces
{
    public interface IDeWiktionaryVocabularyManager : IManager
    {
        List<DeWiktionaryDataModel> SendData(DeWiktionaryDataModel dataModel);
        string GetWordForRequest(int wordId, int cond = 1);
        DeWiktionaryDataModel GetWordParameters(int wordId, int cond = 1);
    }
}
