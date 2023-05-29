using Microsoft.AspNetCore.Mvc;
using NorthwindDBTest.API.Models.Domain;

namespace NorthwindDBTest.API.Repository
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetProductById(int id);
        Task<Product> Create(Product product);
        Task<Product?> Update(int id, Product product);
        Task<Product?> Delete(int id);

    }
}
