using GufoMeParser.DAL.Commands.Interfaces;
using GufoMeParser.DAL.Repository.Interfaces;
using MySql.Data.MySqlClient;

namespace GufoMeParser.DAL.Commands
{
    public class FrAcademicAddWordCommand : IFrAcademicAddWordCommand
    {
        private IRepository _repository { get; set; }

        public FrAcademicAddWordCommand(IRepository repository)
        {
            _repository = repository;
        }

        public async void SendDataAsync(string word)
        {
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter { ParameterName = "", Value = "" }
            };

            try
            {
                await _repository.DbContext.Database.ExecuteSqlCommandAsync("", parameters);
            }
            catch
            {
                return;
            }
        }
    }
}
