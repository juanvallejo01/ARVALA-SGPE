namespace Infrastructure
{
    public class BaseRepository
    {
        protected readonly AppDbContext context;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Beguin()
        {
            await context.Database.BeginTransactionAsync();
        }
        public async Task Comit()
        {
            await context.Database.CommitTransactionAsync();
        }

        public async Task RollBack()
        {
            await context.Database.RollbackTransactionAsync();
        }
    }
}
