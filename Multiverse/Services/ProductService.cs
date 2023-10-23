using Data;
using Entities;
using Multiverse.IServices;

namespace Multiverse.Services
{
    public class ProductService : BaseContextService, IProductService
    {
        public ProductService(ServiceContext serviceContext) : base(serviceContext)
        { 
        }

        public int insertProduct(ProductItem productItem)
        {
            _serviceContext.Products.Add(productItem);
            _serviceContext.SaveChanges();
            return productItem.IdProduct;
        }

        void IProductService.UpdateProduct(ProductItem existingProductItem)
        {
            _serviceContext.Products.Update(existingProductItem);
            _serviceContext.SaveChanges();
        }

        public void DeleteProduct(int IdProduct)
        {
            var product = _serviceContext.Products.Find(IdProduct);
            if (product != null)
            {
                _serviceContext.Products.Remove(product);
                _serviceContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("El producto no existe.");
            }
        }
    }
}
