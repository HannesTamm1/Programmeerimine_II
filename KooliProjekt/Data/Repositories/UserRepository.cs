using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<PagedResult<User>> Get(int id)
        {
            return await DbContext.Users.GetPagedAsync(id, 1);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await DbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<PagedResult<User>> List(int page, int pageSize)
        {
            return await DbContext.Users.GetPagedAsync(page, pageSize);
        }

    }
}
