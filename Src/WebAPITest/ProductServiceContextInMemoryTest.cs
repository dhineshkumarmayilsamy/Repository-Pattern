using AutoMapper;
using Model.Dtos;
using Repository;
using Service;
using System.Collections.Generic;
using Xunit;

namespace WebAPITest
{
    public class ProductServiceContextInMemoryTest
    {

        #region Property  
        // AutoMapper
        private IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile())));
        #endregion

        [Theory]
        [MemberData(nameof(AddProductTestCases))]
        public void AddProduct_Test(ProductDto input, bool expected)
        {
            // Arrange
            TestHelper helper = new TestHelper();
            var productService = new ProductService(new UnitOfWork(helper.Context), mapper);

            // Act
            var output = productService.AddProduct(input);

            // Assert  
            Assert.Equal(expected, output);
        }



        #region Test Cases

        public static IEnumerable<object[]> AddProductTestCases()
        {
            yield return new object[] { new ProductDto() { Id = 0, Name = "Test" }, true };
            yield return new object[] { new ProductDto() { Id = 0, Name = "Test2" }, true };
        }

        # endregion Test Cases
    }
}
