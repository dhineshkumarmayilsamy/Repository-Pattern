using Model.Dto;
using System.Collections.Generic;

namespace Service.Interface
{
    public interface IProductService
    {
        public List<ProductDto> GetAllProducts();
    }
}
