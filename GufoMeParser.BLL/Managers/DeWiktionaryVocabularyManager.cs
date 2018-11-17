using GufoMeParser.BLL.Infrastructure;
using GufoMeParser.BLL.Managers.Interfaces;
using GufoMeParser.Core.BuisinessModels;
using GufoMeParser.Core.ModelsDTO;
using GufoMeParser.DAL.Commands;
using GufoMeParser.DAL.Repository.Interfaces;
using GufoMeParser.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GufoMeParser.BLL.Managers
{
    public class DeWiktionaryVocabularyManager : IDeWiktionaryVocabularyManager
    {
        private IRepository _repository { get; set; }

        public DeWiktionaryVocabularyManager()
        {
            Container.InjectDependencies(this);
        }

        public List<DeWiktionaryDataModel> SendData(DeWiktionaryDataModel dataModel)
        {
            if(dataModel == null)
            {
                Console.WriteLine("Не получилось отправить информацию в DictWordDe, т.к. модель для отправки - пуста!");
                return new List<DeWiktionaryDataModel>();
            }
            var mapperInstance = MapperContainer.MapperInstance;
            var dataModelDTO = mapperInstance.Map<DeWiktionaryDataModel, DeWiktionaryDataModelDTO>(dataModel);
            var sendResponse = new DictWordDeCommand(_repository).SendData(dataModelDTO);
            var responseToBuisiness = new List<DeWiktionaryDataModel>();

            sendResponse.ForEach(dataRow =>
            {
                var buisinessData = mapperInstance.Map<DeWiktionaryDataModelDTO, DeWiktionaryDataModel>(dataRow);
                responseToBuisiness.Add(buisinessData);
            });

            return responseToBuisiness;
        }

        public string GetWordForRequest(int wordId, int cond = 1)
        {
            var wiktionaryModel = new DeWiktionaryDataModelDTO
            {
                WordId = wordId,
                Cond = cond
            };

            var wordForRequest = new DictWordDeCommand(_repository).SendData(wiktionaryModel).FirstOrDefault()?.Word2;

            if(string.IsNullOrEmpty(wordForRequest))
            {
                Console.WriteLine($"Слово с id: {wordId} - не найдено!");
                return string.Empty;
            }

            return wordForRequest;
        }

        public DeWiktionaryDataModel GetWordParameters(int wordId, int cond = 1)
        {
            var wiktionaryModel = new DeWiktionaryDataModelDTO
            {
                WordId = wordId,
                Cond = cond
            };

            var wordParametersDTO = new DictWordDeCommand(_repository).SendData(wiktionaryModel).FirstOrDefault();

            if (wordParametersDTO == null)
            {
                Console.WriteLine($"Слово с id: {wordId} - не найдено!");
                return null;
            }

            var wordParameters = MapperContainer.MapperInstance.Map<DeWiktionaryDataModelDTO, DeWiktionaryDataModel>(wordParametersDTO);

            return wordParameters;
        }
    }
}
