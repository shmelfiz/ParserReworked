using GufoMeParser.BLL.Parsers.GufoMe.Classes;
using GufoMeParser.BLL.Parsers.Interfaces;
using GufoMeParser.BLL.ParsersFactory.Interfaces;
using GufoMeParser.Parsers.BLL.EnAcademic.Classes;
using System;

namespace GufoMeParser.BLL.ParsersFactory.Factory
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

            throw new Exception("Name of type is incorrect!");
        }
    }
}
