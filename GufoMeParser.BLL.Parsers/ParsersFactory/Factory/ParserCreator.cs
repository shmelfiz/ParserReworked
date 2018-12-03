using GufoMeParser.BLL.Parsers.Parsers.DeWiktionary.Classes;
using GufoMeParser.BLL.Parsers.Parsers.FrAcademic.Classes;
using GufoMeParser.BLL.Parsers.Parsers.GufoMe.Classes;
using GufoMeParser.BLL.Parsers.Parsers.Interfaces;
using GufoMeParser.BLL.ParsersFactory.Interfaces;
using GufoMeParser.Parsers.BLL.Parsers.EnAcademic.Classes;
using System;

namespace GufoMeParser.BLL.Parsers.ParsersFactory.Factory
{
    public class ParserCreator : IParserCreator
    {
        public IParser GetParser<T>() where T : IParser
        {
            if (typeof(T) == typeof(GufoParser))
            {
                return new GufoParser();
            }
            if(typeof(T) == typeof(EnAcademicParser))
            {
                return new EnAcademicParser();
            }
            if (typeof(T) == typeof(DeWiktionaryParser))
            {
                return new DeWiktionaryParser();
            }
            if (typeof(T) == typeof(FrAcademicParser))
            {
                return new FrAcademicParser();
            }

            throw new Exception("Name of type is incorrect!");
        }
    }
}
