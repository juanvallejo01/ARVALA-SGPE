using Domain;

namespace Infrastructure.Repositories
{
    public interface IInventarioAlimentoRepository
    {
        Task<InventarioAlimento> GetInventarioByLoteAndTipo(Guid loteId, string tipoAlimento);
        Task<IList<InventarioAlimento>> GetInventariosByLote(Guid loteId);
        Task Save(InventarioAlimento inventario);
        Task Update(InventarioAlimento inventario);
    }
}
