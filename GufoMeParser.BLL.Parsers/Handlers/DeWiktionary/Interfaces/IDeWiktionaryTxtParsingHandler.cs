using GufoMeParser.BLL.Parsers.Handlers.Interfaces;

namespace GufoMeParser.BLL.Parsers.Handlers.DeWiktionary.Interfaces
{
    public interface IDeWiktionaryTxtParsingHandler : IHandler
    {
        new void FillWordParameters();
    }
}
