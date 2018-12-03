using System.Data.Entity;

namespace GufoMeParser.DAL.Context
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MySQLContext : DbContext
    {
    }
}
