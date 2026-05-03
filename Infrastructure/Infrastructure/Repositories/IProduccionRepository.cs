using Domain;

namespace Infrastructure.Repositories
{
    public interface IProduccionRepository
    {
        Task<IList<ProduccionDiaria>> GetProducciones();
        Task<IList<ProduccionDiaria>> GetProduccionesByLote(Guid loteId);
        Task<ProduccionDiaria> GetProduccion(Guid id);
        Task Save(ProduccionDiaria produccion);
    }
}
