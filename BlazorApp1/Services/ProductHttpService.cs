using Blazor.Entities.Models;
using Blazor.Entities.RequestFeatures;
using BlazorApp1.Features;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace BlazorApp1.Services
{
    public class ProductHttpService : IProductHttpService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public ProductHttpService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task CreateProduct(Product product)
        {
            var content = JsonSerializer.Serialize(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("products", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<Product> GetProduct(string id)
        {
            var url = Path.Combine("products", id);
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var product = JsonSerializer.Deserialize<Product>(content, _options);
            return product;
        }

        public async Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters)
        {
            try
            {
                var queryStringParam = new Dictionary<string, string>
                {
                    ["pageNumber"] = productParameters.PageNumber.ToString(),
                    ["searchTerm"] = productParameters.SearchTerm == null ? "" : productParameters.SearchTerm,
                    ["orderBy"] = productParameters.OrderBy
                };

                var response = await _client.GetAsync(QueryHelpers.AddQueryString("Products", queryStringParam));
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }
                var pagingResponse = new PagingResponse<Product>
                {
                    Items = JsonSerializer.Deserialize<List<Product>>(content, _options),
                    MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
                };

                return pagingResponse;
            }
            catch (Exception ex)
            {
                return new PagingResponse<Product>();
            }

        }

        public async Task UpdateProduct(Product product)
        {
            var content = JsonSerializer.Serialize(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("products", product.Id.ToString());
            var putResult = await _client.PutAsync(url, bodyContent);
            var putContent = await putResult.Content.ReadAsStringAsync();
            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }

        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync("https://localhost:5011/api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5011/", postContent);
                return imgUrl;
            }
        }

        public async Task DeleteProduct(Guid id)
        {
            var url = Path.Combine("products", id.ToString());

            var deleteResult = await _client.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();
            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
