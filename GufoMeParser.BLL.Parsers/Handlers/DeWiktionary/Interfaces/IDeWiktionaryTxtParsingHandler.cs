using GufoMeParser.BLL.Parsers.Handlers.Interfaces;

namespace GufoMeParser.BLL.Parsers.Handlers.DeWiktionary.Interfaces
{
    public interface IDeWiktionaryTxtParsingHandler : IHandler
    {
        void FillWordParameters();
    }
}
