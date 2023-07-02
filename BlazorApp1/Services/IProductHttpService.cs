using Blazor.Entities.Models;
using Blazor.Entities.RequestFeatures;
using BlazorApp1.Features;

namespace BlazorApp1.Services
{
    public interface IProductHttpService
    {
        Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters);
        Task CreateProduct(Product product);
        Task<string> UploadProductImage(MultipartFormDataContent content);
        Task<Product> GetProduct(string id);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Guid id);
    }
}
