using System;
using MySql.Data.MySqlClient;
using GufoMeParser.DAL.Commands.Interfaces;
using GufoMeParser.DAL.Repository.Interfaces;

namespace GufoMeParser.DAL.Commands
{
    public class DescrEnCommand : IDescrEnCommand
    {
        private IRepository _repository { get; set; }

        public DescrEnCommand(IRepository repository)
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
                    .ExecuteSqlCommandAsync("call DictWordEn(@Wordfrom, @Descript, @Descript2)", parameters);
            }
            catch(Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
