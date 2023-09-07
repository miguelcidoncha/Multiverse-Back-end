using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Web.Http.Cors;
using Multiverse.IServices;
using Multiverse.Services;


namespace Multiverse.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]


    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ServiceContext _serviceContext;

        public ProductController(IProductService productService, ServiceContext serviceContext)
        {
            _productService = productService;
            _serviceContext = serviceContext;
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
        public int Post([FromBody] ProductItem productItem)
        {
            
            return _productService.insertProduct(productItem);
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public IActionResult Put(int id, [FromBody] ProductItem updatedProductItem)
        {
            // Busca el producto existente por ID
            var existingProductItem = _serviceContext.Products.FirstOrDefault(p => p.IdProduct == id);

            if (existingProductItem == null)
            {
                return NotFound(); // Devuelve 404 si el producto no existe
            }
            else
            {
                // Actualiza los datos del producto existente con los nuevos datos
                existingProductItem.productName = updatedProductItem.productName;
                existingProductItem.productPrice = updatedProductItem.productPrice;
                existingProductItem.productStock = updatedProductItem.productStock;

                // Llama al servicio para guardar los cambios en la base de datos
                try
                {
                    _productService.UpdateProduct(existingProductItem);
                    return Ok(existingProductItem); // Devuelve el producto actualizado
                }
                catch (Exception ex)
                {
                    // Maneja cualquier excepción que pueda ocurrir al guardar
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }
        }





        [HttpDelete("{productId}", Name = "DeleteProduct")]
        public IActionResult Delete(int productId)
        {
            if (productId > 0) // Verifica que el productId sea válido (puedes ajustar la validación según tus requisitos)
            {
                _productService.DeleteProduct(productId);

                return Ok(new { message = "Producto eliminado exitosamente" });
            }
            else
            {
                return BadRequest("El productId no es válido"); // Devuelve un BadRequest en lugar de una excepción
            }
        }
    }
}
