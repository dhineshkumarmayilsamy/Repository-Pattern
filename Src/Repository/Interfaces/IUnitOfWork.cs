using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository Product { get; }
        public int SaveChanges();
        public Task<int> SaveChangesAsync();
    }
}
