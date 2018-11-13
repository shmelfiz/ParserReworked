using GufoMeParser.BLL.Parsers.Interfaces;

namespace GufoMeParser.BLL.ParsersFactory.Interfaces
{
    public interface IParserCreator
    {
        IParser GetParser<T>() where T : IParser;
    }
}
