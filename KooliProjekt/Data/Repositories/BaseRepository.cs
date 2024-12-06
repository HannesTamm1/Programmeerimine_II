namespace KooliProjekt.Data.Repositories
{
    public abstract class BaseRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext DbContext;

        protected BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T?> Get(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task Save(T entity)
        {
            if (entity.Id == 0)
            {
                DbContext.Set<T>().Add(entity);
            }
            else
            {
                DbContext.Set<T>().Update(entity);
            }
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            var entity = await Get(id);
            if (entity != null)
            {
                DbContext.Set<T>().Remove(entity);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
