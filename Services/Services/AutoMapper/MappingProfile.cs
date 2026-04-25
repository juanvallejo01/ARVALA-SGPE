using Domain;
using Mapster;
using Services.Models.CowModels;
using Services.Models.FarmModels;

namespace Services.Automapper
{
    public  class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {//                  src , dest 
            config.NewConfig<Cow, CowModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Milks, src => src.Milks);

            config.NewConfig<CowModel, Cow>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Milks, src => src.Milks);

            config.NewConfig<Farm, FarmModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(des => des.CowCount, src => src.Cows.Count())
                .Map(dest => dest.Location, src => src.Location);

            config.NewConfig<AddFarmModel, Farm>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Location, src => src.Location);
      }
    }
    
}
