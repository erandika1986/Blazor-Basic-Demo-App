using Blazor.Entities.Models;
using BlazorApp1.Services;
using BlazorApp1.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class UpdateProduct
    {
        private Product _product;

        private SuccessNotification _notification;

        [Inject]
        IProductHttpService ProductHttpService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _product = await ProductHttpService.GetProduct(Id);
        }

        private async Task Update()
        {
            await ProductHttpService.UpdateProduct(_product);
            _notification.Show();
        }

        private void AssignImageUrl(string imgUrl) => _product.ImageUrl = imgUrl;
    }
}
