using AutoMapper;
using Model.DomainModel;
using Model.Dtos;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public List<ProductDto> GetAllProducts()
        {
            var products = _unitOfWork.Product.GetAll();
            if (products != null)
                return _mapper.Map<List<ProductDto>>(products);

            return new();
        }

        public void AddProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _unitOfWork.Product.Add(product);
            _unitOfWork.SaveChanges();
        }

        public void UpdateProduct(ProductDto productDto)
        {
            if (productDto != null)
            {
                var productEntity = _unitOfWork.Product.Get(productDto.Id);

                if (productEntity != null)
                {
                    productEntity.Name = productDto.Name;
                    _unitOfWork.Product.Update(productEntity);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    throw new RecordNotFoundException();
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            _unitOfWork.Product.RemoveById(productId);
            _unitOfWork.SaveChanges();
        }
    }
}
