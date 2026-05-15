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
                .AsNoTracking()
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
                await context.SaveChangesAsync();
                await Comit();
            }
            catch
            {
                await RollBack();
                throw;
            }
        }

        public async Task Update(Galpon galpon)
        {
            try
            {
                await Beguin();
                context.Galpones.Update(galpon);
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
