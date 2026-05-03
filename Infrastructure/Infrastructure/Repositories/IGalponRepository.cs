using Domain;

namespace Infrastructure.Repositories
{
    public interface IGalponRepository
    {
        Task<IList<Galpon>> GetGalpones();
        Task<Galpon> GetGalpon(Guid id);
        Task Save(Galpon galpon);
        Task Update(Galpon galpon);
    }
}
