using Domain;

namespace Infrastructure.Repositories
{
    public interface IVacunacionRepository
    {
        Task<IList<RegistroVacunacion>> GetVacunacionesByLote(Guid loteId);
        Task Save(RegistroVacunacion vacunacion);
    }
}
