using Domain;

namespace Infrastructure.Repositories
{
    public interface ILoteRepository
    {
        Task<IList<Lote>> GetLotes();
        Task<Lote> GetLote(Guid id);
        Task<IList<Lote>> GetLotesByGalpon(Guid galponId);
        Task Save(Lote lote);
        Task Update(Lote lote);
    }
}
