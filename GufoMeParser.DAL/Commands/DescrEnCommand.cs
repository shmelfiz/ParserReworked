using System;
using GufoMeParser.BLL.Infrastructure;
using MySql.Data.MySqlClient;
using GufoMeParser.DAL.Commands.Interfaces;
using GufoMeParser.DAL.Repository.Interfaces;

namespace GufoMeParser.DAL.Commands
{
    public class DescrEnCommand : ICommand
    {
        private IRepository _Repository { get; set; }

        public DescrEnCommand()
        {
            _Repository = Container.Resolve<IRepository>();
        }

        public async void SendDataAsync(string word, string parsedTxt, string parsedHtml)
        {
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter { ParameterName = "@Wordfrom", Value = word },
                new MySqlParameter { ParameterName = "@Descript", Value = parsedTxt },
                new MySqlParameter { ParameterName = "@Descript2", Value = parsedHtml },
            };

            try
            {
                await _Repository.DbContext.Database
                    .ExecuteSqlCommandAsync("call DictWordEn(@Wordfrom, @Descript, @Descript2)", parameters);
            }
            catch
            {
                throw new Exception("Проблема записи в БД!");
            }
        }
    }
}
