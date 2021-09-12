using Model.Dtos;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IProductService
    {
        public List<ProductDto> GetAllProducts();
        public void AddProduct(ProductDto productDto);
        public void UpdateProduct(ProductDto productDto);
        public void DeleteProduct(int productId);
    }
}
