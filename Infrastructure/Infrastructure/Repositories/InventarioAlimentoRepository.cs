using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InventarioAlimentoRepository : BaseRepository, IInventarioAlimentoRepository
    {
        public InventarioAlimentoRepository(AppDbContext context) : base(context) { }

        public async Task<InventarioAlimento> GetInventarioByLoteAndTipo(Guid loteId, string tipoAlimento)
        {
            return await context.InventariosAlimento
                .FirstOrDefaultAsync(x => x.LoteId == loteId && x.TipoAlimento == tipoAlimento);
        }

        public async Task<IList<InventarioAlimento>> GetInventariosByLote(Guid loteId)
        {
            return await context.InventariosAlimento
                .Where(x => x.LoteId == loteId)
                .ToListAsync();
        }

        public async Task Save(InventarioAlimento inventario)
        {
            try
            {
                await Beguin();
                await context.InventariosAlimento.AddAsync(inventario);
                await context.SaveChangesAsync();
                await Comit();
            }
            catch
            {
                await RollBack();
                throw;
            }
        }

        public async Task Update(InventarioAlimento inventario)
        {
            try
            {
                await Beguin();
                context.InventariosAlimento.Update(inventario);
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
