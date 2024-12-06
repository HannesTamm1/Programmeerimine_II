namespace KooliProjekt.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(int id);
        Task<PagedResult<User>> List(int page, int pageSize);
        Task Save(User user);
        Task Delete(int id);
    }
}
