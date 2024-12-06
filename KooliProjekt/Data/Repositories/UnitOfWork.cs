using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(
            ApplicationDbContext context,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository,
            IUserRepository userRepository)
        {
            _context = context;

            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
            UserRepository = userRepository;
        }

        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IOrderProductRepository OrderProductRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
