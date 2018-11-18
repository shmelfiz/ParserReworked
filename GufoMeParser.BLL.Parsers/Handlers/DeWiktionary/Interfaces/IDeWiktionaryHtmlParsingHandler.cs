using GufoMeParser.BLL.Parsers.Handlers.Interfaces;

namespace GufoMeParser.BLL.Parsers.Handlers.DeWiktionary.Interfaces
{
    public interface IDeWiktionaryHtmlParsingHandler : IHandler
    {
        new void FillWordParametersHtml();
    }
}
