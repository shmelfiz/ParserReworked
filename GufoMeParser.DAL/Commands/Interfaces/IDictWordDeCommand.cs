using GufoMeParser.Core.ModelsDTO;
using System.Collections.Generic;

namespace GufoMeParser.DAL.Commands.Interfaces
{
    public interface IDictWordDeCommand : ICommand
    {
        List<DeWiktionaryDataModelDTO> SendData(DeWiktionaryDataModelDTO dataModel);
    }
}
