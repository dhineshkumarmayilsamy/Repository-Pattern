

using Model.DomainModel;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Product = new ProductRepository(_context);

        }

        public IProductRepository Product { get; private set; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
