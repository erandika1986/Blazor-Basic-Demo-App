using Blazor.API.Paging;
using Blazor.Entities.Models;
using Blazor.Entities.RequestFeatures;

namespace Blazor.API.Repositories
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetProducts(ProductParameters productParameters);
        Task CreateProduct(Product product);
        Task<Product> GetProduct(Guid id);
        Task UpdateProduct(Product product, Product dbProduct);
        Task DeleteProduct(Product product);
    }
}
