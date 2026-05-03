using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GalponRepository : BaseRepository, IGalponRepository
    {
        public GalponRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Galpon>> GetGalpones()
        {
            return await context.Galpones
                .Include(x => x.Lotes)
                .ToListAsync();
        }

        public async Task<Galpon> GetGalpon(Guid id)
        {
            return await context.Galpones
                .Include(x => x.Lotes)
                .FirstAsync(x => x.Id.Equals(id));
        }

        public async Task Save(Galpon galpon)
        {
            try
            {
                await Beguin();
                await context.Galpones.AddAsync(galpon);
                await Comit();
                await Save();
            }
            catch (Exception ex)
            {
                await RollBack();
                throw ex;
            }
        }

        public async Task Update(Galpon galpon)
        {
            try
            {
                await Beguin();
                await context.SaveChangesAsync();
                await Comit();
            }
            catch (Exception ex)
            {
                await RollBack();
                throw ex;
            }
        }
    }
}
