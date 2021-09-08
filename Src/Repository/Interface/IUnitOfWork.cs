namespace Repository.Interface
{
    public interface IUnitOfWork
    {
        public IProductRepository Product { get; }
        int SaveChanges();
    }
}
