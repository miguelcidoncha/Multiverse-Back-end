using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Web.Http.Cors;
using Multiverse.IServices;
using Multiverse.Services;
using Microsoft.Extensions.Logging;


namespace Multiverse.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]


    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ServiceContext _serviceContext;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ServiceContext serviceContext, ILogger<ProductController> logger)
        {
            _productService = productService;
            _serviceContext = serviceContext;
            _logger = logger;
        }

        [HttpGet(Name = "GetProducts")]
        public IActionResult GetProducts(string productName = null)
        {
            IQueryable<ProductItem> productsQuery = _serviceContext.Set<ProductItem>();

            if (!string.IsNullOrEmpty(productName))
            {
                productsQuery = productsQuery.Where(product => product.productName == productName);
            }

            var products = productsQuery.ToList();
            return Ok(products);
        }



        [HttpPost(Name = "InsertProduct")]
        public IActionResult Post([FromBody] ProductItem productItem)
        {
            try
            {
                var productId = _productService.insertProduct(productItem);

                return Ok(new { IdProduct = productId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }





        [HttpPut("{id}", Name = "UpdateProduct")]
        public IActionResult Put(int id, [FromBody] ProductItem updatedProductItem)
        {
            var existingProductItem = _serviceContext.Products.FirstOrDefault(p => p.IdProduct == id);

            if (existingProductItem == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    
                    if (!string.IsNullOrEmpty(updatedProductItem.ProductImageURL))
                    {
                        existingProductItem.ProductImageURL = updatedProductItem.ProductImageURL;
                    }

                    existingProductItem.productName = updatedProductItem.productName;
                    existingProductItem.productPrice = updatedProductItem.productPrice;
                    existingProductItem.productStock = updatedProductItem.productStock;

                    _productService.UpdateProduct(existingProductItem);

                    return Ok(existingProductItem);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }
        }





        [HttpDelete("{productId}", Name = "DeleteProduct")]
        public IActionResult Delete(int productId)
        {
            if (productId > 0) 
            {
                _productService.DeleteProduct(productId);

                return Ok(new { message = "Producto eliminado exitosamente" });
            }
            else
            {
                return BadRequest("El productId no es válido"); 
            }
        }
    }
}
