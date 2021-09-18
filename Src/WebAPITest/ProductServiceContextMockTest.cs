using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Model.DomainModel;
using Model.Dtos;
using Moq;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace WebAPITest
{
    public class ProductServiceContextMockTest
    {
        #region Property  
        private Mock<AppDbContext> dbContextMock = new Mock<AppDbContext>();
        private Mock<DbSet<Product>> dbSetMock = new Mock<DbSet<Product>>();

        // AutoMapper
        private IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile())));
        #endregion


        [Fact]
        public void GetAllProducts_Test()
        {
            // Arrange
            var data = new List<Product> { new Product() { Id = 1, Name = "Test" } };

            var queryable = dbSetMock.As<IQueryable<Product>>();
            queryable.Setup(s => s.GetEnumerator()).Returns(() => data.GetEnumerator());

            dbContextMock.Setup(s => s.Set<Product>()).Returns(dbSetMock.Object);

            var productService = new ProductService(new UnitOfWork(dbContextMock.Object), mapper);

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
            dbSetMock.Setup(s => s.Find(1)).Returns(new Product() { Id = 1, Name = "Test" });
            dbContextMock.Setup(s => s.Set<Product>()).Returns(dbSetMock.Object);
            var productService = new ProductService(new UnitOfWork(dbContextMock.Object), mapper);

            // Act
            var product = productService.GetProductById(productId);

            // Assert  
            expProduct.Should().BeEquivalentTo(product);
        }

        [Theory]
        [MemberData(nameof(AddProductTestCases))]
        public void AddProduct_Test(ProductDto input, bool expected)
        {
            // Arrange
            dbSetMock.Setup(s => s.Add(It.IsAny<Product>()));
            dbContextMock.Setup(s => s.Set<Product>()).Returns(dbSetMock.Object);
            dbContextMock.Setup(s => s.SaveChanges()).Returns(1);

            var productService = new ProductService(new UnitOfWork(dbContextMock.Object), mapper);

            // Act
            var output = productService.AddProduct(input);

            // Assert  
            Assert.Equal(expected, output);
        }

        [Theory]
        [MemberData(nameof(AddProductNullTestCases))]
        public void AddProductNull_Test(ProductDto input)
        {

            // Arrange
            dbSetMock.Setup(s => s.Add(It.IsAny<Product>()));
            dbContextMock.Setup(s => s.Set<Product>()).Returns(dbSetMock.Object);
            dbContextMock.Setup(s => s.SaveChanges()).Returns(1);

            var productService = new ProductService(new UnitOfWork(dbContextMock.Object), mapper);

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

        public static IEnumerable<object[]> AddProductTestCases()
        {
            yield return new object[] { new ProductDto() { Id = 0, Name = "Test" }, true };
            yield return new object[] { new ProductDto() { Id = 0, Name = "Test2" }, true };
        }

        public static IEnumerable<object[]> AddProductNullTestCases()
        {
            yield return new object[] { null };
        }
        # endregion Test Cases
    }
}
