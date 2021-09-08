using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Dto;
using Service.Interface;
using System;


namespace WebAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ILogger _logger;
        public ProductController(
            IProductService productService,
            ILogger logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/<HomeController>
        [HttpGet]
        public ResponseModel GetAllProducts()
        {
            try
            {
                var messages = _productService.GetAllProducts();
                return new ResponseModel()
                {
                    Data = messages,
                    Status = ResponseCode.Success
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ResponseModel()
                {
                    Data = null,
                    Status = ResponseCode.Error
                };
            }
        }
    }
}
