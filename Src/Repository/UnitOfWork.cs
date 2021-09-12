

using Model.DomainModel;
using Repository.Interfaces;

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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
