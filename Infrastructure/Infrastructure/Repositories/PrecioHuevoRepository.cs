using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PrecioHuevoRepository : BaseRepository, IPrecioHuevoRepository
    {
        public PrecioHuevoRepository(AppDbContext context) : base(context) { }

        public async Task<IList<PrecioHuevo>> GetPrecios()
        {
            return await context.PreciosHuevo.OrderBy(x => x.Clasificacion).ToListAsync();
        }

        public async Task<PrecioHuevo?> GetPrecio(Guid id)
        {
            return await context.PreciosHuevo.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(PrecioHuevo precio)
        {
            try
            {
                await Beguin();
                await context.PreciosHuevo.AddAsync(precio);
                await context.SaveChangesAsync();
                await Comit();
            }
            catch
            {
                await RollBack();
                throw;
            }
        }

        public async Task Update(PrecioHuevo precio)
        {
            try
            {
                await Beguin();
                context.PreciosHuevo.Update(precio);
                await context.SaveChangesAsync();
                await Comit();
            }
            catch
            {
                await RollBack();
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await Beguin();
                var precio = await context.PreciosHuevo.FindAsync(id);
                if (precio != null) context.PreciosHuevo.Remove(precio);
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
