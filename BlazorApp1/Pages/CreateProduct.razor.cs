using Blazor.Entities.Models;
using BlazorApp1.Services;
using BlazorApp1.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class CreateProduct
    {
        private Product _product = new Product();
        private SuccessNotification _notification;

        private void AssignImageUrl(string imgUrl) => _product.ImageUrl = imgUrl;

        [Inject]
        public IProductHttpService ProductService { get; set; }
        private async Task Create()
        {
            await ProductService.CreateProduct(_product);
            _notification.Show();
        }
    }
}
