using AutoMapper;
using Model.Dto;
using Repository.Interface;
using Service.Interface;
using System.Collections.Generic;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public List<ProductDto> GetAllProducts()
        {
            var products = _unitOfWork.Product.GetAll();
            if (products != null)
                return _mapper.Map<List<ProductDto>>(products);

            return new();
        }
    }
}
