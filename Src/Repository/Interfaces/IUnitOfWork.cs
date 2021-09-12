namespace Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository Product { get; }
        int SaveChanges();
    }
}
