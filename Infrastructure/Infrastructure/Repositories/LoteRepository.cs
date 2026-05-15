using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoteRepository : BaseRepository, ILoteRepository
    {
        public LoteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Lote>> GetLotes()
        {
            return await context.Lotes
                .Include(x => x.Galpon)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Lote> GetLote(Guid id)
        {
            return await context.Lotes
                .Include(x => x.Galpon)
                .Include(x => x.Producciones)
                .Include(x => x.Inventarios)
                .FirstAsync(x => x.Id.Equals(id));
        }

        public async Task<IList<Lote>> GetLotesByGalpon(Guid galponId)
        {
            return await context.Lotes
                .AsNoTracking()
                .Where(x => x.GalponId == galponId)
                .ToListAsync();
        }

        public async Task Save(Lote lote)
        {
            try
            {
                await Beguin();
                await context.Lotes.AddAsync(lote);
                await context.SaveChangesAsync();
                await Comit();
            }
            catch
            {
                await RollBack();
                throw;
            }
        }

        public async Task Update(Lote lote)
        {
            try
            {
                await Beguin();
                context.Lotes.Update(lote);
                await context.SaveChangesAsync();
                await Comit();
            }
            catch
            {
                await RollBack();
                throw;
            }
        }
    }
}
