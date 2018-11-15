using System;
using GufoMeParser.DAL.Commands.Interfaces;
using GufoMeParser.DAL.Repository.Interfaces;
using MySql.Data.MySqlClient;

namespace GufoMeParser.DAL.Commands
{
    public class DescrRuCommand : IDescrRuCommand
    {
        private IRepository _repository { get; set; }

        public DescrRuCommand(IRepository repository)
        {
            _repository = repository;
        }

        public async void SendDataAsync(string word, string parsedTxt, string parsedHtml)
        {
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter { ParameterName = "@Wordfrom", Value = word },
                new MySqlParameter { ParameterName = "@Descript", Value = parsedTxt },
                new MySqlParameter { ParameterName = "@Descript2", Value = parsedHtml }
            };

            try
            {
                await _repository.DbContext.Database
                    .ExecuteSqlCommandAsync("call DictWordRu(@Wordfrom, @Descript, @Descript2)", parameters);
            }
            catch
            {
                throw new Exception("Проблема записи в БД!");
            }
        }
    }
}
