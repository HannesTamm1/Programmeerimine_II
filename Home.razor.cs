using KooliProjekt.PublicAPI.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KooliProjekt.BlazorApp.Pages
{
    public partial class Home
    {
        [Inject]
        protected IApiClient ApiClient { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        private List<Product> products;

        protected override async Task OnInitializedAsync()
        {
            var result = await this.ApiClient.List(); // Explicitly use 'this' to resolve ambiguity  

            products = result.Value;
        }

        protected async Task Delete(int id)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
            if (!confirmed)
            {
                return;
            }

            await this.ApiClient.Delete(id); // Explicitly use 'this' to resolve ambiguity  

            NavManager.Refresh();
        }
    }
}
