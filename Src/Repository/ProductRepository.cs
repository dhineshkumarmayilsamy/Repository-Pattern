using Model.DomainModel;
using Repository.Interfaces;

namespace Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext context) : base(context)
        {

        }
        public AppDbContext Context
        {
            get { return _dbContext as AppDbContext; }
        }
    }
}
