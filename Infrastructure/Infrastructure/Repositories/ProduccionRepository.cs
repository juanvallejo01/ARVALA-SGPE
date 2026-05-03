using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProduccionRepository : BaseRepository, IProduccionRepository
    {
        public ProduccionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<ProduccionDiaria>> GetProducciones()
        {
            return await context.ProduccionesDiarias
                .Include(x => x.Lote)
                .OrderByDescending(x => x.Fecha)
                .ToListAsync();
        }

        public async Task<IList<ProduccionDiaria>> GetProduccionesByLote(Guid loteId)
        {
            return await context.ProduccionesDiarias
                .Where(x => x.LoteId == loteId)
                .OrderBy(x => x.Fecha)
                .ToListAsync();
        }

        public async Task<ProduccionDiaria> GetProduccion(Guid id)
        {
            return await context.ProduccionesDiarias
                .Include(x => x.Lote)
                .FirstAsync(x => x.Id.Equals(id));
        }

        public async Task Save(ProduccionDiaria produccion)
        {
            try
            {
                await Beguin();
                await context.ProduccionesDiarias.AddAsync(produccion);
                await Comit();
                await Save();
            }
            catch (Exception ex)
            {
                await RollBack();
                throw ex;
            }
        }
    }
}
