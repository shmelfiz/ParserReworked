using GufoMeParser.Core.ModelsDTO;
using GufoMeParser.DAL.Commands.Interfaces;
using GufoMeParser.DAL.Repository.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GufoMeParser.DAL.Commands
{
    public class DictWordDeCommand : IDictWordDeCommand
    {
        private IRepository _repository { get; set; }

        public DictWordDeCommand(IRepository repository)
        {
            _repository = repository;
        }

        public List<DeWiktionaryDataModelDTO> SendData(DeWiktionaryDataModelDTO dataModel)
        {
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter { ParameterName = "@WordId", Value = dataModel.WordId },
                new MySqlParameter { ParameterName = "@Word2", Value = dataModel.Word2 },
                new MySqlParameter { ParameterName = "@Transcr", Value = dataModel.Transcr },
                new MySqlParameter { ParameterName = "@PartOfSpeech", Value = dataModel.PartOfSpeech },
                new MySqlParameter { ParameterName = "@PartOfSpeechSeit", Value = dataModel.PartOfSpeechSeit },
                new MySqlParameter { ParameterName = "@Descript", Value = dataModel.Descript },
                new MySqlParameter { ParameterName = "@Example", Value = dataModel.Example },
                new MySqlParameter { ParameterName = "@WordForms", Value = dataModel.WordForms },
                new MySqlParameter { ParameterName = "@Cond", Value = dataModel.Cond }
            };

            try
            {
                var response = _repository.DbContext.Database
                    .SqlQuery<DeWiktionaryDataModelDTO>("call DictWordDe(@WordId, @Word2, @Transcr, @PartOfSpeech, @PartOfSpeechSeit, @Descript, @Example, @WordForms, @Cond)", parameters).ToList();

                return response;
            }
            catch
            {
                throw new Exception("Проблема записи в БД!");
            }
        }
    }
}
