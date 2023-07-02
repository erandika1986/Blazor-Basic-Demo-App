using Blazor.Entities.Models;
using Blazor.Entities.RequestFeatures;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class Products: IDisposable
    {
        public List<Product> ProductList { get; set; } = new List<Product>();
        public MetaData MetaData { get; set; } = new MetaData();
        private ProductParameters _productParameters = new ProductParameters();

        [Inject]
        public IProductHttpService ProductService { get; set; }

        [Inject]
        public HttpInterceptorService Interceptor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            var pagingResponse = await ProductService.GetProducts(_productParameters);
            //just for testing
            ProductList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetProducts();
        }
        private async Task GetProducts()
        {
            var pagingResponse = await ProductService.GetProducts(_productParameters);
            ProductList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _productParameters.PageNumber = 1;
            _productParameters.SearchTerm = searchTerm;
            await GetProducts();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _productParameters.OrderBy = orderBy;
            await GetProducts();
        }

        private async Task DeleteProduct(Guid id)
        {
            await ProductService.DeleteProduct(id);
            _productParameters.PageNumber = 1;
            await GetProducts();
        }

        public void Dispose()
        {
            Interceptor.DisposeEvent();
        }
    }
}
