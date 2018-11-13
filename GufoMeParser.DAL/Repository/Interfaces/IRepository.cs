using GufoMeParser.DAL.Context;

namespace GufoMeParser.DAL.Repository.Interfaces
{
    public interface IRepository
    {
        MySQLContext DbContext { get; }
    }
}
