namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderProductRepository OrderProductRepository { get; }
        IUserRepository UserRepository { get; }

        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
