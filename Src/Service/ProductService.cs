using AutoMapper;
using Model.DomainModel;
using Model.Dtos;
using Repository.Interfaces;
using Service.Interfaces;
using System;
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

        public ProductDto GetProductById(int productId)
        {
            var product = _unitOfWork.Product.Get(productId);
            if (product != null)
            {
                return _mapper.Map<ProductDto>(product);
            }
            return null;
        }

        public bool AddProduct(ProductDto productDto)
        {
            if (productDto != null)
            {
                var product = _mapper.Map<Product>(productDto);
                _unitOfWork.Product.Add(product);
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                throw new ArgumentNullException();
            }
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
