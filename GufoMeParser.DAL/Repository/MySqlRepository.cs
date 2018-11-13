using GufoMeParser.DAL.Context;
using GufoMeParser.DAL.Repository.Interfaces;
using System;

namespace GufoMeParser.DAL.Repository
{
    public class MySqlRepository : IRepository
    {
        public MySQLContext DbContext { get; private set; }

        public MySqlRepository()
        {
            DbContext = new MySQLContext();
        }
    }
}
