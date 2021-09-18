using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(
            IProductService productService,
            ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<ProductDto>), 200)]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(new ResponseModel()
                {
                    Result = products,
                    Status = ResponseCode.Success
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel()
                    {
                        Result = null,
                        Status = ResponseCode.Error
                    });
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        public IActionResult GetProductById(int productId)
        {
            try
            {
                var product = _productService.GetProductById(productId);

                if (product != null)
                {
                    return Ok(new ResponseModel()
                    {
                        Result = product,
                        Status = ResponseCode.Success
                    });
                }
                else
                {
                    return NotFound(new ResponseModel()
                    {
                        Result = null,
                        Status = ResponseCode.Failed,
                        ErrorCode = "RecordNotFound",
                        ErrorMessage = "RecordNotFound"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel()
                    {
                        Result = null,
                        Status = ResponseCode.Error
                    });
            }
        }

        [HttpPost("[action]")]
        public IActionResult AddProduct(ProductDto productDto)
        {
            try
            {
                if (productDto?.Id != 0)
                {
                    return BadRequest(new ResponseModel()
                    {
                        Result = null,
                        Status = ResponseCode.Failed
                    });
                }

                _productService.AddProduct(productDto);

                return Ok(new ResponseModel()
                {
                    Result = null,
                    Status = ResponseCode.Success
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ResponseModel()
                   {
                       Result = null,
                       Status = ResponseCode.Error
                   });
            }
        }

        [HttpPut("[action]")]
        public IActionResult UpdateProduct(ProductDto productDto)
        {
            try
            {
                _productService.UpdateProduct(productDto);
                return Ok(new ResponseModel()
                {
                    Result = null,
                    Status = ResponseCode.Success
                });

            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(new ResponseModel()
                {
                    Result = null,
                    Status = ResponseCode.Failed,
                    ErrorCode = "RecordNotFoundException",
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ResponseModel()
                   {
                       Result = null,
                       Status = ResponseCode.Error
                   });
            }
        }

        [HttpDelete("[action]")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                _productService.DeleteProduct(productId);
                return Ok(new ResponseModel()
                {
                    Result = null,
                    Status = ResponseCode.Success
                });

            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(new ResponseModel()
                {
                    Result = null,
                    Status = ResponseCode.Failed,
                    ErrorCode = "RecordNotFoundException",
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ResponseModel()
                   {
                       Result = null,
                       Status = ResponseCode.Error
                   });
            }
        }
    }
}
