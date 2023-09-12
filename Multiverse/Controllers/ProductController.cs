﻿using Data;
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


        [HttpGet("ByCategory/{categoryId}", Name = "GetProductsByCategoryId")]
        public IActionResult GetProductsByCategoryId(int categoryId)
        {
            IQueryable<ProductItem> productsQuery = _serviceContext.Set<ProductItem>();

            if (categoryId > 0)
            {
                productsQuery = productsQuery.Where(product => product.IdCategories == categoryId);
            }
            else
            {
                return BadRequest("El categoryId no es válido");
            }

            var products = productsQuery.ToList();
            return Ok(products);
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

        public int Post([FromQuery] string userUser_Name, [FromQuery] string userPassword, [FromBody] ProductItem productItem)
        {
            var seletedUser = _serviceContext.Set<UserItem>()
                               .Where(u => u.UserName == userUser_Name
                                    && u.Password == userPassword
                                    && u.IdRol == 1)
                                .FirstOrDefault();


            if (seletedUser != null)
            {
                return _productService.insertProduct(productItem);

            }
            else
            {
                throw new InvalidCredentialException("Usuario no permitido");
            }
        }





        [HttpPut("{id}", Name = "UpdateProduct")]
        public IActionResult Put(int id, [FromBody] ProductItem updatedProductItem, [FromQuery] string userUser_Name, [FromQuery] string userPassword)
        {
            var selectedUser = _serviceContext.Set<UserItem>()
               .Where(u => u.UserName == userUser_Name
                    && u.Password == userPassword
                    && u.IdRol == 1)
               .FirstOrDefault();

            if (selectedUser == null)
            {
                throw new InvalidCredentialException("Usuario no permitido");
            }

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
        public IActionResult Delete(int productId, [FromQuery] string userUser_Name, [FromQuery] string userPassword)
        {
            var selectedUser = _serviceContext.Set<UserItem>()
               .Where(u => u.UserName == userUser_Name
                    && u.Password == userPassword
                    && u.IdRol == 1)
               .FirstOrDefault();

            if (selectedUser == null)
            {
                throw new InvalidCredentialException("Usuario no permitido");
            }

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