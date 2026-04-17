using Domain;
using Infrastructure.Repositories;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Services.Models.CowModels;
using Services.Models.FarmModels;

namespace Services
{
    public class FarmService : IFarmService
    {

        private IFarmRepository FarmRepository{ get; set; }
        private IMapper Mapper { get; set; }
        private IConfiguration Configuration { get; set; }
        public FarmService(IMapper mapper, IConfiguration configuration, IFarmRepository farmRepository)
        {
            
            FarmRepository = farmRepository;
            Configuration = configuration;
            Mapper = mapper;
        }
        public async Task <IList<FarmModel>> GetFarms()
        {
            var farms = await FarmRepository.GetFarms();
            var model = Mapper.Map<IList<FarmModel>>(farms);
            return model;

        }

        public async Task AddFarm(AddFarmModel model)
        {
            await FarmRepository.Save(Mapper.Map<Farm>(model));
        }

        public async Task<FarmModel> GetFarm(Guid id)
        {
            return  Mapper.Map<FarmModel> (await FarmRepository.GetFarm(id));
        }

        public async Task AddCow(AddCowModel model)
        {
            var farm = await FarmRepository.GetFarm(model.FarmId);
            farm.Cows.Add(Mapper.Map<Cow>(model));
            await FarmRepository.UpDate(farm);
        }
    }
}
