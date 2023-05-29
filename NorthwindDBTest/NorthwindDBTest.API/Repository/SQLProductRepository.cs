using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindDBTest.API.Models.Data;
using NorthwindDBTest.API.Models.Domain;

namespace NorthwindDBTest.API.Repository
{
    public class SQLProductRepository : IProductsRepository
    {
        private readonly NorthwindDbContext dbContext;

        public SQLProductRepository(NorthwindDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> Create(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> Delete(int id)
        {
            var choosedProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (choosedProduct == null)
            {
                return null;
            }
            dbContext.Products.Remove(choosedProduct);
            await dbContext.SaveChangesAsync();
            return choosedProduct;

        }

        public async Task<List<Product>> GetAll()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<Product?> Update(int id, Product product)
        {
            var productDomain = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (productDomain == null)
            {
                return null;
            }

            productDomain.ProductName = product.ProductName;
            productDomain.QuantityPerUnit = product.QuantityPerUnit;
            productDomain.UnitPrice = product.UnitPrice;
            productDomain.UnitsInStock = product.UnitsInStock;
            productDomain.UnitsOnOrder = product.UnitsOnOrder;
            productDomain.ReorderLevel = product.ReorderLevel;
            productDomain.Discontinued = product.Discontinued;

            await dbContext.SaveChangesAsync();
            return productDomain;
           
    }
    }
}
