using AutoMapper;
using GufoMeParser.Core.BuisinessModels;
using GufoMeParser.Core.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GufoMeParser.Infrastructure
{
    public static class MapperInitializer
    {
        public static IMapper MapperInstance { get; private set; }

        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DeWiktionaryDataModel, DeWiktionaryDataModelDTO>()
                .ForMember(destin => destin.Cond, map => map.MapFrom(source => source.Condition))
                .ForMember(destin => destin.Descript, map => map.MapFrom(source => source.Description))
                .ForMember(destin => destin.Example, map => map.MapFrom(source => source.Example))
                .ForMember(destin => destin.PartOfSpeech, map => map.MapFrom(source => source.PartOfSpeech))
                .ForMember(destin => destin.PartOfSpeechSeit, map => map.MapFrom(source => source.PartOfSpeechSeit))
                .ForMember(destin => destin.Transcr, map => map.MapFrom(source => source.Transcription))
                .ForMember(destin => destin.Word2, map => map.MapFrom(source => source.CurrentWord))
                .ForMember(destin => destin.WordForms, map => map.MapFrom(source => source.WordForms))
                .ForMember(destin => destin.WordId, map => map.MapFrom(source => source.WordId));
                cfg.CreateMap<DeWiktionaryDataModel, DeWiktionaryDataModelDTO>()
                .ForMember(destin => destin.Cond, map => map.MapFrom(source => source.Condition))
                .ForMember(destin => destin.Descript, map => map.MapFrom(source => source.Description))
                .ForMember(destin => destin.Example, map => map.MapFrom(source => source.Example))
                .ForMember(destin => destin.PartOfSpeech, map => map.MapFrom(source => source.PartOfSpeech))
                .ForMember(destin => destin.PartOfSpeechSeit, map => map.MapFrom(source => source.PartOfSpeechSeit))
                .ForMember(destin => destin.Transcr, map => map.MapFrom(source => source.Transcription))
                .ForMember(destin => destin.Word2, map => map.MapFrom(source => source.CurrentWord))
                .ForMember(destin => destin.WordForms, map => map.MapFrom(source => source.WordForms))
                .ForMember(destin => destin.WordId, map => map.MapFrom(source => source.WordId)).ReverseMap();
            });
        }
    }
}
