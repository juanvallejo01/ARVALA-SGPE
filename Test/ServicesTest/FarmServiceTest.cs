using Domain;
using Infrastructure.Repositories;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Services;
using Services.Models.FarmModels;

namespace ServicesTest
{
    public class FarmServiceTest
    {
        private IFarmService farmService {  get; set; }
        //se hace mock de las clases de dependencia
        Mock<IFarmRepository> milkRepositoryMock { get; set; }
        Mock<IMapper> mapperMock { get; set; }
        Farm farm { get; set; }
        //IMapper mapper { get; set; }

        [SetUp]
        public void Setup()
        {
            farm = new Farm();
            farm.Id = Guid.NewGuid();
            //se implementa el mock y se hace setup  de los metodos requeridos para la prueba
            milkRepositoryMock = new Mock<IFarmRepository>();
            milkRepositoryMock.Setup(x => x.GetFarm(It.IsAny<Guid>())).ReturnsAsync(farm);
           mapperMock = new Mock<IMapper>();
           mapperMock.Setup(x => x.Map<FarmModel>(It.IsAny<Farm>())).Returns(new FarmModel { Id = farm.Id, Name = "my Farm" });

            //mapper = new MapperConfiguration(cfg => new MappingProfile() ).CreateMapper();
            //no s epuede hacer mock de esta clase se instanci con los parametros requeridos
            Dictionary<string, string> inMemorySettings = new Dictionary<string, string> 
            {
              {"AllowedHosts", "*"},
              {"ConnectionStrings:Defaultconnection", "Server=localhost,1433;Database=ApiTestDb;user id=SA;password=Passw0rd1ns3c;TrustServerCertificate=True"},
                //...populate as needed for the test
            };
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            farmService = new FarmService(mapperMock.Object,configuration, milkRepositoryMock.Object);
        }

        [Test]
        public void MilkService_Exist()
        {
            Assert.That(farmService, Is.Not.Null);
            var result = farmService.GetFarm(farm.Id);
            Assert.That(result.Result.Id, Is.EqualTo(farm.Id));
        }
    }
}