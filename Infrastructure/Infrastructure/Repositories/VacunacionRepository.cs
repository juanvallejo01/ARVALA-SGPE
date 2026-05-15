using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VacunacionRepository : BaseRepository, IVacunacionRepository
    {
        public VacunacionRepository(AppDbContext context) : base(context) { }

        public async Task<IList<RegistroVacunacion>> GetVacunacionesByLote(Guid loteId)
        {
            return await context.RegistrosVacunacion
                .Where(x => x.LoteId == loteId)
                .OrderByDescending(x => x.Fecha)
                .ToListAsync();
        }

        public async Task Save(RegistroVacunacion vacunacion)
        {
            try
            {
                await Beguin();
                await context.RegistrosVacunacion.AddAsync(vacunacion);
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
