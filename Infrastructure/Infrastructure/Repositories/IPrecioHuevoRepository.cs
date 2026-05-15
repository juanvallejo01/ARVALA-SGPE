using Domain;

namespace Infrastructure.Repositories
{
    public interface IPrecioHuevoRepository
    {
        Task<IList<PrecioHuevo>> GetPrecios();
        Task<PrecioHuevo?> GetPrecio(Guid id);
        Task Save(PrecioHuevo precio);
        Task Update(PrecioHuevo precio);
        Task Delete(Guid id);
    }
}
