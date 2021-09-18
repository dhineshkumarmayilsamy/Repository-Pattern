using AutoMapper;
using FluentAssertions;
using Model.DomainModel;
using Model.Dtos;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace WebAPITest
{
    public class ProductServiceContextTest
    {

        #region Property  
        // AutoMapper
        private IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile())));
        #endregion


        [Fact]
        public void GetAllProducts_Test()
        {
            // Arrange
            var productService = new ProductService(new UnitOfWork(new AppDbContext()), mapper);

            // Act
            var products = productService.GetAllProducts();

            // Assert  
            Assert.NotNull(products);
            Assert.IsAssignableFrom<List<ProductDto>>(products);
            Assert.True(products.Count > 0);
        }

        [Theory]
        [MemberData(nameof(GetProductByIdTestCases))]
        public void GetProductById_Test(int productId, ProductDto expProduct)
        {
            // Arrange
            var productService = new ProductService(new UnitOfWork(new AppDbContext()), mapper);

            // Act
            var product = productService.GetProductById(productId);

            // Assert  
            expProduct.Should().BeEquivalentTo(product);
        }

        [Theory]
        [MemberData(nameof(AddProductNullTestCases))]
        public void AddProductNull_Test(ProductDto input)
        {

            // Arrange
            var productService = new ProductService(new UnitOfWork(new AppDbContext()), mapper);

            // Act
            Action act = () => productService.AddProduct(input);

            // Assert  
            act.Should().Throw<ArgumentNullException>();
        }


        #region Test Cases
        public static IEnumerable<object[]> GetProductByIdTestCases()
        {
            yield return new object[] { 1, new ProductDto() { Id = 1, Name = "Test" } };
            yield return new object[] { -1, null };
        }

        public static IEnumerable<object[]> AddProductNullTestCases()
        {
            yield return new object[] { null };
        }
        # endregion Test Cases
    }
}
